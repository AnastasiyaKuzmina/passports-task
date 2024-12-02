using PassportApplication.Results;
using PassportApplication.Services.Interfaces;

namespace PassportApplication.Services
{
    /// <summary>
    /// Implements IUpdateService
    /// </summary>
    public class UpdateService : IUpdateService
    {
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
        public UpdateService(IFileDownloadService fileDownloadService, IUnpackService unpackService, ICopyService copyService)
        {
            _fileDownloadService = fileDownloadService;
            _unpackService = unpackService;
            _copyService = copyService;
        }

        /// <summary>
        /// Updates database 
        /// </summary>
        /// <returns>Result instance</returns>
        public async Task<Result> UpdateAsync(CancellationToken cancellationToken)
        {
            var fileDownloadResult = await _fileDownloadService.DownloadFileAsync(cancellationToken);

            if (fileDownloadResult.IsSuccess == false) return fileDownloadResult;

            var unpackResult = _unpackService.Unpack();
            if (unpackResult.IsSuccess == false) return unpackResult;

            var copyResult = await _copyService.CopyAsync(cancellationToken);
            if (copyResult.IsSuccess == false) return copyResult;

            return Result.Ok();
        }
    }
}
