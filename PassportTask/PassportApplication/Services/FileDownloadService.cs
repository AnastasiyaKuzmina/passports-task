using PassportApplication.Services.Interfaces;

namespace PassportApplication.Services
{
    public class FileDownloadService : IFileDownloadService
    {
        public async Task DownloadFile(string url, string DirectoryPath, string FilePath)
        {
            if (!Directory.Exists(DirectoryPath))
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
