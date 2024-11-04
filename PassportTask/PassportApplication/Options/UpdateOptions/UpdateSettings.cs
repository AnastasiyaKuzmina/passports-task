using PassportApplication.Services.Interfaces;

namespace PassportApplication.Options.UpdateOptions
{
    /// <summary>
    /// Update settings class
    /// </summary>
    public class UpdateSettings
    {
        /// <summary>
        /// File URL to download
        /// </summary>
        public string FileUrl { get; set; }
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
            FileUrl = GetFileUrl(configuration);
            DirectoryPath = GetDirectoryPath(configuration);
            FilePath = GetFilePath(configuration);
            ExtractPath = GetExtractPath(configuration);
        }

        private string GetFileUrl(IConfiguration configuration)
        {
            var path = configuration.GetSection("DatabaseUpdate").GetSection("FileUrl").Value;
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