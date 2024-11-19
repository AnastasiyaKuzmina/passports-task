using System.IO.Compression;
using PassportApplication.Results;
using PassportApplication.Services.Interfaces;
using Microsoft.Extensions.Options;
using PassportApplication.Options;

namespace PassportApplication.Services
{
    /// <summary>
    /// Implements IUnpackService
    /// </summary>
    public class UnpackService : IUnpackService
    {
        private readonly Settings _settings;
        public UnpackService(IOptions<Settings> settings) 
        {
            _settings = settings.Value;
        }

        /// <summary>
        /// Unpacks the file
        /// </summary>
        /// <param name="FilePath">File path</param>
        /// <param name="ExtractPath">Extract path</param>
        /// <returns>Result instance</returns>
        public Result Unpack()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _settings.UpdateSettings.Directory, _settings.UpdateSettings.File);
            var extractPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _settings.UpdateSettings.Directory, _settings.UpdateSettings.Extract);

            if (!File.Exists(filePath))
            {
                return Result.Fail("File for unpack doesn't exist");
            }

            ZipFile.ExtractToDirectory(filePath, extractPath);
            return Result.Ok();
        }
    }
}
