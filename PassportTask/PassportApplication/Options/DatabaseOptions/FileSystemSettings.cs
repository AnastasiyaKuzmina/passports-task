using PassportApplication.Options.DatabaseOptions.Interfaces;

namespace PassportApplication.Options.DatabaseOptions
{
    public class FileSystemSettings : IDatabaseSettings
    {
        public string DatabasePath { get; set; }
        public string PassportsPath { get; set; }
        public string PassportsHistoryPath { get; set; }

        public FileSystemSettings(IConfiguration configuration)
        {
            DatabasePath = GetDatabasePath(configuration);
            PassportsPath = GetPassportsPath(configuration);
            PassportsHistoryPath = GetPassportsHistoryPath(configuration);
        }

        private string GetDatabasePath(IConfiguration configuration)
        {
            var directory = configuration.GetSection("FileSystemDatabase").GetSection("Directory").Value;
            var database = configuration.GetSection("FileSystemDatabase").GetSection("Database").Value;
            if ((directory == null) || (database == null)) throw new NotImplementedException();

            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, directory, database);
        }

        private string GetPassportsPath(IConfiguration configuration)
        {
            var path = configuration.GetSection("FileSystemDatabase").GetSection("Passports").Value;
            if (path == null) throw new NotImplementedException();

            return Path.Combine(DatabasePath, path);
        }

        private string GetPassportsHistoryPath(IConfiguration configuration)
        {
            var path = configuration.GetSection("FileSystemDatabase").GetSection("PassportsHistory").Value;
            if (path == null) throw new NotImplementedException();

            return Path.Combine(DatabasePath, path);
        }
    }
}