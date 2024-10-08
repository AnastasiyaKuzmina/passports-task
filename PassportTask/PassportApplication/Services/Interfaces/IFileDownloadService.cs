namespace PassportApplication.Services.Interfaces
{
    public interface IFileDownloadService
    {
        public Task DownloadFile(string url, string DirectoryPath, string FilePath);
    }
}
