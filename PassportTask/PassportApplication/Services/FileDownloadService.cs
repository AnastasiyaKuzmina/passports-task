using YandexDisk.Client.Clients;
using YandexDisk.Client.Http;
using YandexDisk.Client.Protocol;

using PassportApplication.Results;
using PassportApplication.Services.Interfaces;
using PassportApplication.Options.YandexOptions;

namespace PassportApplication.Services
{
    /// <summary>
    /// IImplements IFileDownloadService
    /// </summary>
    public class FileDownloadService : IFileDownloadService
    {
        /// <summary>
        /// Downloads a csv file
        /// </summary>
        /// <param name="yandexToken">Yandex token</param>
        /// <param name="yandexDirectory">Yandex disk directory</param>
        /// <param name="yandexFileName">Yandex file name</param>
        /// <param name="directoryPath">Directory path</param>
        /// <param name="filePath">File path</param>
        /// <returns>Result instance</returns>
        public async Task<Result> DownloadFileAsync(YandexSettings yandexSettings, string directoryPath, string filePath, CancellationToken cancellationToken)
        {
            if (Directory.Exists(directoryPath))
            {
                Directory.Delete(directoryPath, true);
            }
            Directory.CreateDirectory(directoryPath);

            var apiConnection = new DiskHttpApi(yandexSettings.Token);
            if (apiConnection == null) return Result.Fail("Incorrect Yandex Disk token");

            var rootFolderData = await apiConnection.MetaInfo.GetInfoAsync(new ResourceRequest
            {
                Path = "/" + yandexSettings.Directory + "/"
            }, cancellationToken);

            if (rootFolderData == null) return Result.Fail("Incorrect Yandex Disk path");

            Resource? data = rootFolderData.Embedded.Items.FirstOrDefault(i => i.Name == yandexSettings.FileName);
            if (data == null) return Result.Fail("No Data.zip file in Yandex disk");

            await apiConnection.Files.DownloadFileAsync(data.Path, filePath, cancellationToken);

            return Result.Ok();

            //using (var httpClient = new HttpClient())
            //{
            //    using (var response = await httpClient.GetAsync(url))
            //    {
            //        if (response.IsSuccessStatusCode == false)
            //        {
            //            return new Result(new Error(ErrorType.HttpClientError, "Unable to download the file"));
            //        }
            //        using (var fileStream = new FileStream(FilePath, FileMode.Create, FileAccess.Write, FileShare.None))
            //        {
            //            await response.Content.CopyToAsync(fileStream);
            //        }
            //    }
            //}
        }
    }
}
