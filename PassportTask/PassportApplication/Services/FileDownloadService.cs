using PassportApplication.Results;
using PassportApplication.Errors;
using PassportApplication.Errors.Enums;
using PassportApplication.Services.Interfaces;
using YandexDisk.Client.Http;
using YandexDisk.Client.Protocol;
using YandexDisk.Client.Clients;

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
        /// <param name="YandexToken">Yandex token</param>
        /// <param name="DirectoryPath">Directory path</param>
        /// <param name="FilePath">File path</param>
        /// <returns></returns>
        public async Task<Result> DownloadFileAsync(string yandexToken, string yandexDirectory, string yandexFileName, string directoryPath, string filePath)
        {
            if (Directory.Exists(directoryPath))
            {
                Directory.Delete(directoryPath, true);
            }
            Directory.CreateDirectory(directoryPath);

            var apiConnection = new DiskHttpApi(yandexToken);
            if (apiConnection == null) return new Result(new Error(ErrorType.YandexDiskError, "Incorrect Yandex Disk token"));

            var rootFolderData = await apiConnection.MetaInfo.GetInfoAsync(new ResourceRequest
            {
                Path = "/" + yandexDirectory + "/"
            });

            if (rootFolderData == null) return new Result(new Error(ErrorType.YandexDiskError, "Incorrect Yandex Disk path"));

            Resource? data = rootFolderData.Embedded.Items.Where(i => i.Name == yandexFileName).FirstOrDefault();
            if (data == null) return new Result(new Error(ErrorType.YandexDiskError, "No Data.zip file in Yandex disk"));

            await apiConnection.Files.DownloadFileAsync(data.Path, filePath);

            return new Result();

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
