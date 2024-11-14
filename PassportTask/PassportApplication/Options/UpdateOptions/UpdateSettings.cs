namespace PassportApplication.Options.UpdateOptions
{
    /// <summary>
    /// Update settings class
    /// </summary>
    public class UpdateSettings
    {
        /// <summary>
        /// Yandex Disk Token
        /// </summary>
        public string YandexDiskToken { get; set; }
        /// <summary>
        /// Yandex Disk file directory
        /// </summary>
        public string YandexDiskDirectory { get; set; }
        /// <summary>
        /// Yandex Disk file name
        /// </summary>
        public string YandexDiskFileName { get; set; }
        /// <summary>
        /// Directory path to download
        /// </summary>
        public string DirectoryPath { get; set; }
        /// <summary>
        /// File path to download
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// Extract path
        /// </summary>
        public string ExtractPath { get; set; }

        /// <summary>
        /// Constructor of UpdateSettings
        /// </summary>
        /// <param name="configuration"></param>
        public UpdateSettings(IConfiguration configuration)
        {
            YandexDiskToken = GetYandexDiskToken(configuration);
            YandexDiskDirectory = GetYandexDiskDirectory(configuration);
            YandexDiskFileName = GetYandexDiskFileName(configuration);
            DirectoryPath = GetDirectoryPath(configuration);
            FilePath = GetFilePath(configuration);
            ExtractPath = GetExtractPath(configuration);
        }

        private string GetYandexDiskToken(IConfiguration configuration)
        {
            var token = configuration.GetSection("DatabaseUpdate").GetSection("YandexDisk").GetSection("Token").Value;
            if (token == null) throw new NotImplementedException();

            return token;
        }

        private string GetYandexDiskDirectory(IConfiguration configuration)
        {
            var path = configuration.GetSection("DatabaseUpdate").GetSection("YandexDisk").GetSection("Directory").Value;
            if (path == null) throw new NotImplementedException();

            return path;
        }

        private string GetYandexDiskFileName(IConfiguration configuration)
        {
            var path = configuration.GetSection("DatabaseUpdate").GetSection("YandexDisk").GetSection("FileName").Value;
            if (path == null) throw new NotImplementedException();

            return path;
        }

        private string GetDirectoryPath(IConfiguration configuration)
        {
            var path = configuration.GetSection("DatabaseUpdate").GetSection("Directory").Value;
            if (path == null) throw new NotImplementedException();

            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
        }

        private string GetFilePath(IConfiguration configuration)
        {
            var path = configuration.GetSection("DatabaseUpdate").GetSection("ZipFile").Value;
            if (path == null) throw new NotImplementedException();

            return Path.Combine(DirectoryPath, path);
        }

        private string GetExtractPath(IConfiguration configuration)
        {
            var path = configuration.GetSection("DatabaseUpdate").GetSection("ExtractDirectory").Value;
            if (path == null) throw new NotImplementedException();

            return Path.Combine(DirectoryPath, path);
        }
    }
}