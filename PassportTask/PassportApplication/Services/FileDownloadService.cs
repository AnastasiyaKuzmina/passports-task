using PassportApplication.Services.Interfaces;

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
        /// <param name="url">File's url</param>
        /// <param name="DirectoryPath">Directory path</param>
        /// <param name="FilePath">File path</param>
        /// <returns></returns>
        public async Task DownloadFileAsync(string url, string DirectoryPath, string FilePath)
        {
            if (Directory.Exists(DirectoryPath) == false)
            {
                Directory.CreateDirectory(DirectoryPath);
            }
            else
            {
                Directory.Delete(DirectoryPath, true);
                Directory.CreateDirectory(DirectoryPath);
            }

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetStreamAsync(url))
                {
                    using (var fileStream = new FileStream(FilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        await response.CopyToAsync(fileStream);
                    }
                }
            }
        }
    }
}
