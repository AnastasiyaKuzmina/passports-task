using PassportApplication.Options;
using PassportApplication.Results;
using PassportApplication.Services.Interfaces;

namespace PassportApplication.Services
{
    /// <summary>
    /// Implements IUpdateService
    /// </summary>
    public class UpdateService : IUpdateService
    {
        private readonly Settings _settings;
        private readonly IFileDownloadService _fileDownloadService;
        private readonly IUnpackService _unpackService;
        private readonly ICopyService _copyService;

        /// <summary>
        /// Constructor of UpdateService
        /// </summary>
        /// <param name="fileDownloadService">File download service</param>
        /// <param name="unpackService">File unpack service</param>
        /// <param name="parserService">Parser service</param>
        /// <param name="databaseService">Database update service</param>
        public UpdateService(Settings settings, IFileDownloadService fileDownloadService, IUnpackService unpackService, ICopyService copyService)
        {
            _settings = settings;
            _fileDownloadService = fileDownloadService;
            _unpackService = unpackService;
            _copyService = copyService;
        }

        /// <summary>
        /// Updates database 
        /// </summary>
        /// <returns>Result instance</returns>
        public async Task<Result> UpdateAsync()
        {
            var fileDownloadResult = await _fileDownloadService.DownloadFileAsync(_settings.UpdateSettings.YandexSettings, 
                _settings.UpdateSettings.DirectoryPath, 
                _settings.UpdateSettings.FilePath);

            if (fileDownloadResult.IsSuccess == false) return fileDownloadResult;

            var unpackResult = await _unpackService.UnpackAsync(_settings.UpdateSettings.FilePath, _settings.UpdateSettings.ExtractPath);
            if (unpackResult.IsSuccess == false) return unpackResult;
                
            var copyResult = await _copyService.CopyAsync(Directory.GetFiles(_settings.UpdateSettings.ExtractPath)[0], _settings.FormatSettings);
            if (copyResult.IsSuccess == false) return copyResult;

            return Result.Ok();
        }
    }
}
