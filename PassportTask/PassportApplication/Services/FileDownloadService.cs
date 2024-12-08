using Microsoft.Extensions.Options;

using YandexDisk.Client.Clients;
using YandexDisk.Client.Http;
using YandexDisk.Client.Protocol;

using PassportApplication.Results;
using PassportApplication.Services.Interfaces;
using PassportApplication.Options.UpdateOptions;
using PassportApplication.Options.YandexOptions;

namespace PassportApplication.Services
{
    /// <summary>
    /// IImplements IFileDownloadService
    /// </summary>
    public class FileDownloadService : IFileDownloadService
    {
        private readonly UpdateSettings _updateSettings;
        private readonly YandexSettings _yandexSettings;

        public FileDownloadService(IOptions<UpdateSettings> updateSettings, IOptions<YandexSettings> yandexSettings)
        {
            _updateSettings = updateSettings.Value;
            _yandexSettings = yandexSettings.Value;
        }

        /// <summary>
        /// Downloads a csv file
        /// </summary>
        /// <param name="yandexToken">Yandex token</param>
        /// <param name="yandexDirectory">Yandex disk directory</param>
        /// <param name="yandexFileName">Yandex file name</param>
        /// <param name="directoryPath">Directory path</param>
        /// <param name="filePath">File path</param>
        /// <returns>Result instance</returns>
        public async Task<Result> DownloadFileAsync(CancellationToken cancellationToken)
        {
            var directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _updateSettings.Directory);
            var filePath = Path.Combine(directoryPath, _updateSettings.File);

            if (Directory.Exists(directoryPath))
            {
                Directory.Delete(directoryPath, true);
            }
            Directory.CreateDirectory(directoryPath);

            var apiConnection = new DiskHttpApi(_yandexSettings.Token);
            if (apiConnection == null) return Result.Fail("Incorrect Yandex Disk token");

            var rootFolderData = await apiConnection.MetaInfo.GetInfoAsync(new ResourceRequest
            {
                Path = "/" + _yandexSettings.Directory + "/"
            }, cancellationToken);

            if (rootFolderData == null) return Result.Fail("Incorrect Yandex Disk path");

            Resource? data = rootFolderData.Embedded.Items.FirstOrDefault(i => 
            i.Name == _yandexSettings.FileName);
            if (data == null) return Result.Fail("No Data.zip file in Yandex disk");

            await apiConnection.Files.DownloadFileAsync(data.Path, filePath, cancellationToken);

            return Result.Ok();
        }
    }
}
