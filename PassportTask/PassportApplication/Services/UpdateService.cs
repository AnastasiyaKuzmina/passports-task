using PassportApplication.Services.Interfaces;

namespace PassportApplication.Services
{
    /// <summary>
    /// Implements IUpdateService
    /// </summary>
    public class UpdateService : IUpdateService
    {
        private readonly IConfiguration _configuration;
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
        public UpdateService(IConfiguration configuration, IFileDownloadService fileDownloadService, IUnpackService unpackService, ICopyService copyService)
        {
            _configuration = configuration;
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
            string? FileUrl = _configuration.GetSection("DatabaseUpdate").GetSection("FileUrl").Value;
            if (FileUrl == null) return;

            string? DirectoryPath = _configuration.GetSection("DatabaseUpdate").GetSection("DirectoryPath").Value;
            if (DirectoryPath == null) return;

            string? ZipPath = _configuration.GetSection("DatabaseUpdate").GetSection("ZipPath").Value;
            if (ZipPath == null) return;

            string? ExtractFolder = _configuration.GetSection("DatabaseUpdate").GetSection("ExtractPath").Value;
            if (ExtractFolder == null) return;

            string FilePath = DirectoryPath + ZipPath;
            string ExtractPath = DirectoryPath + ExtractFolder;

            // await _fileDownloadService.DownloadFileAsync(FileUrl, DirectoryPath, FilePath);
            // await _unpackService.UnpackAsync(FilePath, ExtractPath);
            await _copyService.CopyAsync(Directory.GetFiles(ExtractPath)[0]);
        }
    }
}
