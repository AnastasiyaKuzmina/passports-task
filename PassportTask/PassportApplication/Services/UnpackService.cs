using System.IO.Compression;
using Microsoft.Extensions.Options;

using PassportApplication.Results;
using PassportApplication.Services.Interfaces;
using PassportApplication.Options.UpdateOptions;

namespace PassportApplication.Services
{
    /// <summary>
    /// Implements IUnpackService
    /// </summary>
    public class UnpackService : IUnpackService
    {
        private readonly UpdateSettings _updateSettings;
        public UnpackService(IOptions<UpdateSettings> settings) 
        {
            _updateSettings = settings.Value;
        }

        /// <summary>
        /// Unpacks the file
        /// </summary>
        /// <param name="FilePath">File path</param>
        /// <param name="ExtractPath">Extract path</param>
        /// <returns>Result instance</returns>
        public Result Unpack()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _updateSettings.Directory, _updateSettings.File);
            var extractPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _updateSettings.Directory, _updateSettings.Extract);

            if (!File.Exists(filePath))
            {
                return Result.Fail("File for unpack doesn't exist");
            }

            ZipFile.ExtractToDirectory(filePath, extractPath);
            return Result.Ok();
        }
    }
}
