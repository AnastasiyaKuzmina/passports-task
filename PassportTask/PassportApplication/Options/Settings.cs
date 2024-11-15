﻿using PassportApplication.Options.DatabaseOptions;
using PassportApplication.Options.DatabaseOptions.Interfaces;
using PassportApplication.Options.Enums;
using PassportApplication.Options.FormatOptions;
using PassportApplication.Options.UpdateOptions;

namespace PassportApplication.Options
{
    /// <summary>
    /// Settings class
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// Database mode
        /// </summary>
        public DatabaseMode DatabaseMode { get; }

        /// <summary>
        /// Database settings 
        /// </summary>
        public IDatabaseSettings DatabaseSettings { get; }

        /// <summary>
        /// Update settings
        /// </summary>
        public UpdateSettings UpdateSettings { get; }

        /// <summary>
        /// Format settings
        /// </summary>
        public FormatSettings FormatSettings { get; }

        /// <summary>
        /// Constructor of Settings
        /// </summary>
        /// <param name="configuration"></param>
        public Settings(IConfiguration configuration) 
        { 
            DatabaseMode = GetDatabaseMode(configuration);
            DatabaseSettings = GetDatabaseSettings(configuration);
            UpdateSettings = new UpdateSettings(configuration);
            FormatSettings = new FormatSettings();
        }

        private DatabaseMode GetDatabaseMode(IConfiguration configuration)
        {
            var stringDatabaseMode = configuration.GetSection("Database").Value ?? "";

            if (Enum.TryParse(stringDatabaseMode, out DatabaseMode databaseMode) == false)
            {
                throw new NotImplementedException();
            }

            return databaseMode;
        }

        private IDatabaseSettings GetDatabaseSettings(IConfiguration configuration)
        {
            switch (DatabaseMode)
            {
                case DatabaseMode.FileSystem:
                    return new FileSystemSettings(configuration);
                case DatabaseMode.PostgreSql:
                    return new PostgreSqlSettings(configuration);
                case DatabaseMode.MsSql:
                    return new MsSqlSettings(configuration);
                default: 
                    throw new NotImplementedException();
            }
        }
    }
}
