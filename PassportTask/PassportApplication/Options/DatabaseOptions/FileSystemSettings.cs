﻿using PassportApplication.Options.DatabaseOptions.Interfaces;

namespace PassportApplication.Options.DatabaseOptions
{
    /// <summary>
    /// Implements IDatabaseSettings
    /// </summary>
    public class FileSystemSettings : IDatabaseSettings
    {
        /// <summary>
        /// Database path
        /// </summary>
        public string DatabasePath { get; set; }
        /// <summary>
        /// Passports path
        /// </summary>
        public string PassportsPath { get; set; }
        /// <summary>
        /// Passports history path
        /// </summary>
        public string PassportsHistoryPath { get; set; }

        /// <summary>
        /// Constructor of FileSystemSettings
        /// </summary>
        /// <param name="configuration">Application configuration</param>
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