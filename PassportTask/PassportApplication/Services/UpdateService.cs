using PassportApplication.Options.UpdateOptions;
using PassportApplication.Services.Interfaces;
using System.Diagnostics;

namespace PassportApplication.Services
{
    /// <summary>
    /// Implements IUpdateService
    /// </summary>
    public class UpdateService : IUpdateService
    {
        private readonly UpdateSettings _updateSettings;
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
        public UpdateService(UpdateSettings updateSettings, IFileDownloadService fileDownloadService, IUnpackService unpackService, ICopyService copyService)
        {
            _updateSettings = updateSettings;
            _fileDownloadService = fileDownloadService;
            _unpackService = unpackService;
            _copyService = copyService;
        }

        /// <summary>
        /// Updates database 
        /// </summary>
        /// <returns></returns>
        public async Task UpdateAsync()
        {
            //await _fileDownloadService.DownloadFileAsync(_updateSettings.FileUrl, _updateSettings.DirectoryPath, _updateSettings.FilePath);
            //await _unpackService.UnpackAsync(_updateSettings.FilePath, _updateSettings.ExtractPath);
            await _copyService.CopyAsync(Directory.GetFiles(_updateSettings.ExtractPath)[0]);
        }
    }
}
