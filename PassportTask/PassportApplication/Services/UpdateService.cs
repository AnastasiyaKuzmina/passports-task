using PassportApplication.Models;
using PassportApplication.Services.Interfaces;

namespace PassportApplication.Services
{
    public class UpdateService : IUpdateService
    {
        const string FileUrl = "https://www.learningcontainer.com/wp-content/uploads/2020/05/sample-zip-file.zip";
        const string DirectoryPath = "./Files";
        const string ZipName = "Passports.zip";
        const string ExtractPath = DirectoryPath + "/File/";
        const string FilePath = DirectoryPath + "/" + ZipName;

        private readonly IConfiguration _configuration;
        private readonly IFileDownloadService _fileDownloadService;
        private readonly IUnpackService _unpackService;
        private readonly IParserService _parserService;
        private readonly IDatabaseService _databaseService;

        public UpdateService(IFileDownloadService fileDownloadService, IUnpackService unpackService,
                            IParserService parserService, IDatabaseService databaseService, 
                            IConfiguration configuration)
        {
            _fileDownloadService = fileDownloadService;
            _unpackService = unpackService;
            _parserService = parserService;
            _databaseService = databaseService;
            _configuration = configuration;
        }

        public async Task Update()
        {
            await _fileDownloadService.DownloadFile(FileUrl, DirectoryPath, FilePath);
            await _unpackService.Unpack(FilePath, ExtractPath);

            List<Passport> passports = await _parserService.Parse(Directory.GetFiles(ExtractPath)[0]);

            await _databaseService.Update(passports);
        }
    }
}
