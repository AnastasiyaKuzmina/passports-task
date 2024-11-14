using System.IO.Compression;
using PassportApplication.Results;
using PassportApplication.Errors;
using PassportApplication.Errors.Enums;
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
        /// <returns></returns>
        public async Task<Result> UnpackAsync(string FilePath, string ExtractPath)
        {
            if (File.Exists(FilePath) == false)
            {
                return new Result(new Error(ErrorType.FileDoesNotExist, "File for unpack doesn't exist"));
            }

            await Task.Run(() => ZipFile.ExtractToDirectory(FilePath, ExtractPath));
            return new Result();
        }
    }
}
