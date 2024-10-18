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
        /// <param name="url">File's url</param>
        /// <param name="DirectoryPath">Directory path</param>
        /// <param name="FilePath">File path</param>
        /// <returns></returns>
        public Task DownloadFileAsync(string url, string DirectoryPath, string FilePath);
    }
}
