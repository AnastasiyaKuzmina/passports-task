using System.IO.Compression;
using PassportApplication.Results;
using PassportApplication.Services.Interfaces;

namespace PassportApplication.Services
{
    /// <summary>
    /// Implements IUnpackService
    /// </summary>
    public class UnpackService : IUnpackService
    {
        /// <summary>
        /// Unpacks the file
        /// </summary>
        /// <param name="FilePath">File path</param>
        /// <param name="ExtractPath">Extract path</param>
        /// <returns>Result instance</returns>
        public async Task<Result> UnpackAsync(string filePath, string extractPath)
        {
            if (File.Exists(filePath) == false)
            {
                return Result.Fail("File for unpack doesn't exist");
            }

            await Task.Run(() => ZipFile.ExtractToDirectory(filePath, extractPath));
            return Result.Ok();
        }
    }
}
