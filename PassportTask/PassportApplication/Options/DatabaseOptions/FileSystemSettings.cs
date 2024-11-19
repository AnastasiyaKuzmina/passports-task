using PassportApplication.Options.DatabaseOptions.Interfaces;

namespace PassportApplication.Options.DatabaseOptions
{
    /// <summary>
    /// Implements IDatabaseSettings
    /// </summary>
    public record FileSystemSettings
    {
        /// <summary>
        /// Directory
        /// </summary>
        public string Directory { get; init; }

        /// <summary>
        /// Database name
        /// </summary>
        public string Database { get; init; }

        /// <summary>
        /// Current passports path: true if PassportsPath, false if PassportsPath2
        /// </summary>
        public bool CurrentPassportsPath { get; set; } = true;

        /// <summary>
        /// Passports path 1
        /// </summary>
        public string Passports1 { get; init; }

        /// <summary>
        /// Passports path 2
        /// </summary>
        public string Passports2 { get; init; }

        /// <summary>
        /// Passports template path
        /// </summary>
        public string PassportsTemplate { get; init; }

        /// <summary>
        /// Passports history path
        /// </summary>
        public string PassportsHistory { get; init; }

        /// <summary>
        /// File name format
        /// </summary>
        public string FileNameFormat { get; init; }

        ///// <summary>
        ///// Constructor of FileSystemSettings
        ///// </summary>
        ///// <param name="configuration">Application configuration</param>
        //public FileSystemSettings(IConfiguration configuration)
        //{
        //    DatabasePath = GetDatabasePath(configuration);
        //    PassportsPath1 = GetPassportsPath1(configuration);
        //    PassportsPath2 = GetPassportsPath2(configuration);
        //    PassportsTemplatePath = GetPassportsTemplatePath(configuration);
        //    PassportsHistoryPath = GetPassportsHistoryPath(configuration);
        //    FileNameFormat = GetFileNameFormat(configuration);
        //}

        //private string GetDatabasePath(IConfiguration configuration)
        //{
        //    var directory = configuration.GetSection("FileSystemDatabase").GetSection("Directory").Value;
        //    var database = configuration.GetSection("FileSystemDatabase").GetSection("Database").Value;
        //    if ((directory == null) || (database == null)) throw new NotImplementedException();

        //    return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, directory, database);
        //}

        //private string GetPassportsPath1(IConfiguration configuration)
        //{
        //    var path = configuration.GetSection("FileSystemDatabase").GetSection("Passports1").Value;
        //    if (path == null) throw new NotImplementedException();

        //    return Path.Combine(DatabasePath, path);
        //}

        //private string GetPassportsPath2(IConfiguration configuration)
        //{
        //    var path = configuration.GetSection("FileSystemDatabase").GetSection("Passports2").Value;
        //    if (path == null) throw new NotImplementedException();

        //    return Path.Combine(DatabasePath, path);
        //}

        //private string GetPassportsTemplatePath(IConfiguration configuration)
        //{
        //    var path = configuration.GetSection("FileSystemDatabase").GetSection("PassportsTemplate").Value;
        //    if (path == null) throw new NotImplementedException();

        //    return Path.Combine(DatabasePath, path);
        //}

        //private string GetPassportsHistoryPath(IConfiguration configuration)
        //{
        //    var path = configuration.GetSection("FileSystemDatabase").GetSection("PassportsHistory").Value;
        //    if (path == null) throw new NotImplementedException();

        //    return Path.Combine(DatabasePath, path);
        //}

        //private string GetFileNameFormat(IConfiguration configuration)
        //{
        //    var format = configuration.GetSection("FileSystemDatabase").GetSection("FileNameFormat").Value;
        //    if (format == null) throw new NotImplementedException();

        //    return format;
        //}
    }
}