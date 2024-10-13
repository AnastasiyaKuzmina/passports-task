using System.IO.Compression;
using PassportApplication.Services.Interfaces;

namespace PassportApplication.Services
{
    /// <summary>
    /// File unpack service
    /// </summary>
    public class UnpackService : IUnpackService
    {
        /// <summary>
        /// Unpacks the file
        /// </summary>
        /// <param name="FilePath">File path</param>
        /// <param name="ExtractPath">Extract path</param>
        /// <returns></returns>
        public async Task Unpack(string FilePath, string ExtractPath)
        {
            await Task.Run(() => ZipFile.ExtractToDirectory(FilePath, ExtractPath));
        }
    }
}
