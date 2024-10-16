using PassportApplication.Models;
using PassportApplication.Services.Interfaces;

namespace PassportApplication.Services
{
    /// <summary>
    /// Update database service
    /// </summary>
    public class UpdateService : IUpdateService
    { 
        //const string DirectoryPath = "./Files";
        //const string ExtractPath = DirectoryPath + "/File/";
        //const string FilePath = DirectoryPath + "/Passports.zip";

        private readonly IConfiguration _configuration;
        private readonly IFileDownloadService _fileDownloadService;
        private readonly IUnpackService _unpackService;
        private readonly IDatabaseService _databaseService;

        /// <summary>
        /// Constructor of UpdateService
        /// </summary>
        /// <param name="fileDownloadService">File download service</param>
        /// <param name="unpackService">File unpack service</param>
        /// <param name="parserService">Parser service</param>
        /// <param name="databaseService">Database update service</param>
        public UpdateService(IConfiguration configuration, IFileDownloadService fileDownloadService, IUnpackService unpackService, IDatabaseService databaseService)
        {
            _configuration = configuration;
            _fileDownloadService = fileDownloadService;
            _unpackService = unpackService;
            _databaseService = databaseService;
        }

        /// <summary>
        /// Updates database 
        /// </summary>
        /// <returns></returns>
        public async Task Update()
        {
            string FileUrl = _configuration.GetSection("DatabaseUpdate").GetSection("FileUrl").Value;
            string DirectoryPath = _configuration.GetSection("DatabaseUpdate").GetSection("DirectoryPath").Value;
            string FilePath = DirectoryPath + _configuration.GetSection("DatabaseUpdate").GetSection("ZipPath").Value;
            string ExtractPath = DirectoryPath + _configuration.GetSection("DatabaseUpdate").GetSection("ExtractPath").Value;

            await _fileDownloadService.DownloadFile(FileUrl, DirectoryPath, FilePath);
            await _unpackService.Unpack(FilePath, ExtractPath);
            await _databaseService.UpdateAsync();
        }
    }
}
