using PassportApplication.Models;
using PassportApplication.Services.Interfaces;

namespace PassportApplication.Services
{
    /// <summary>
    /// Update database service
    /// </summary>
    public class UpdateService : IUpdateService
    {
        static readonly string[] FilesUrls = {"https://downloader.disk.yandex.ru/disk/e95e9ce2ea831b6b17e3700461f5b077801adb545e143345f620d0348f458c0c/670bb3f7/CYUE6SZbbZhfY6qMgEkQq5F7Sa5wS5UkCgPQHo5niSLAWUoUyMVNZnJlpkOXmaVlKF9Pximehh7MZ-EBLGVX_Q%3D%3D?uid=0&filename=Data1.zip&disposition=attachment&hash=n46w%2BNZxQdpdA%2BWOJvYm1L3%2BM27aYS4VwSKT8LP2IaG4DvLCMb/BXLIKTwxmndd8q/J6bpmRyOJonT3VoXnDag%3D%3D&limit=0&content_type=application%2Fzip&owner_uid=120867976&fsize=2688&hid=ed95e465e44633aeb00a3f7d24d3c76d&media_type=compressed&tknv=v2",
                                                "https://downloader.disk.yandex.ru/disk/9c1ddf2f97f8e0ed9d43dc997eab633a8a6ce14d02ed79abc2c4f02a3f9237de/670bb4e9/CYUE6SZbbZhfY6qMgEkQq5n-xwR9afCSuRYPj_G2nf-2GreAZQzy5-O5G3k61kAHEJOt0e1Vy2qA0sS26Wglig%3D%3D?uid=0&filename=Data2.zip&disposition=attachment&hash=DKSog8pDoOQND3wdeo7XDAfg/Y0k0QeCQN2tyvvAHoVtzttb7ZeuKZ09dXNboFlhq/J6bpmRyOJonT3VoXnDag%3D%3D&limit=0&content_type=application%2Fzip&owner_uid=120867976&fsize=2663&hid=a644a97b124824857fd3575820ccf694&media_type=compressed&tknv=v2",
                                                "https://downloader.disk.yandex.ru/disk/bb0387e72a84a7630b55338010933fc80a1016f937b3fac2041ac52a2b6e67f4/670bb526/CYUE6SZbbZhfY6qMgEkQq-9YX8Zqe0prX9z21iNzCC0JnK_VMIx8SfLDsLunRsPho1Ow99fLmh7FTs5VO8q-WQ%3D%3D?uid=0&filename=Data3.zip&disposition=attachment&hash=grGGnSePZSJLyzmMCSSPNVeg7VdXigW8U03de9OsnZWeANBjnWnAC74CyCvDFM1zq/J6bpmRyOJonT3VoXnDag%3D%3D&limit=0&content_type=application%2Fzip&owner_uid=120867976&fsize=2693&hid=a3e663eabbd71e300612e883ef94b270&media_type=compressed&tknv=v2",
                                                "https://downloader.disk.yandex.ru/disk/55cc9840931e70bc15f958a055ac380b1a44efdf15a984771f845dfcef3d28ea/670bb55c/CYUE6SZbbZhfY6qMgEkQq3jcW8UMdhKqYb3ZDrvLhEhsh1x2p3jpmpT4Be8age3cA-RCLd-k7AcAGXg0dcSGXg%3D%3D?uid=0&filename=Data4.zip&disposition=attachment&hash=R79pJCzh3vstJaPzXHcBjoK6huczJCDxYY61D0NzY0FkK2bi0wl5bp1Xserk1MMYq/J6bpmRyOJonT3VoXnDag%3D%3D&limit=0&content_type=application%2Fzip&owner_uid=120867976&fsize=2692&hid=63519b62d169d2785ca468a635b6f5c1&media_type=compressed&tknv=v2"};
        const string DirectoryPath = "./Files";
        const string ExtractPath = DirectoryPath + "/File/";
        const string FilePath = DirectoryPath + "/Passports.zip";
        static int index = 0;

        private readonly IFileDownloadService _fileDownloadService;
        private readonly IUnpackService _unpackService;
        private readonly IParserService _parserService;
        private readonly IDatabaseService _databaseService;

        /// <summary>
        /// Constructor of UpdateService
        /// </summary>
        /// <param name="fileDownloadService">File download service</param>
        /// <param name="unpackService">File unpack service</param>
        /// <param name="parserService">Parser service</param>
        /// <param name="databaseService">Database update service</param>
        public UpdateService(IFileDownloadService fileDownloadService, IUnpackService unpackService,
                            IParserService parserService, IDatabaseService databaseService)
        {
            _fileDownloadService = fileDownloadService;
            _unpackService = unpackService;
            _parserService = parserService;
            _databaseService = databaseService;
        }

        /// <summary>
        /// Updates database 
        /// </summary>
        /// <returns></returns>
        public async Task Update()
        {
            if (index > 3)
            {
                return;
            }

            await _fileDownloadService.DownloadFile(FilesUrls[index], DirectoryPath, FilePath);
            await _unpackService.Unpack(FilePath, ExtractPath);

            List<Passport> passports = await _parserService.Parse(Directory.GetFiles(ExtractPath)[0]);
            index++;

            await _databaseService.Update(passports);
        }
    }
}
