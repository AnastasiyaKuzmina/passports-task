﻿namespace PassportApplication.Services.Interfaces
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
        public Task DownloadFile(string url, string DirectoryPath, string FilePath);
    }
}
