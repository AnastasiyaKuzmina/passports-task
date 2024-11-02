using PassportApplication.Services.Interfaces;

namespace PassportApplication.Options.UpdateOptions
{
    public class UpdateSettings
    {
        public string FileUrl { get; set; }
        public string DirectoryPath { get; set; }
        public string FilePath { get; set; }
        public string ExtractPath { get; set; }

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