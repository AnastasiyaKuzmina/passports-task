using PassportApplication.Results;

namespace PassportApplication.Services.Interfaces
{
    /// <summary>
    /// File download interface
    /// </summary>
    public interface IFileDownloadService
    {
        /// <summary>
        /// Downloads a csv file
        /// </summary>
        /// <param name="yandexToken">Yandex token</param>
        /// <param name="yandexDirectory">Yandex disk directory</param>
        /// <param name="yandexFileName">Yandex file name</param>
        /// <param name="directoryPath">Directory path</param>
        /// <param name="filePath">File path</param>
        /// <returns></returns>
        public Task<Result> DownloadFileAsync(string yandexToken, string yandexDirectory, string yandexFileName, string directoryPath, string filePath);
    }
}
