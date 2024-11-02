using PassportApplication.Options.Enums;
using PassportApplication.Options.DatabaseOptions;
using PassportApplication.Options.UpdateOptions;
using PassportApplication.Options.DatabaseOptions.Interfaces;

namespace PassportApplication.Options
{
    public class Settings
    {
        public DatabaseMode DatabaseMode { get; }
        public IDatabaseSettings DatabaseSettings { get; }
        public UpdateSettings UpdateSettings { get; }

        public Settings(IConfiguration configuration) 
        { 
            DatabaseMode = GetDatabaseMode(configuration);
            DatabaseSettings = GetDatabaseSettings(configuration);
            UpdateSettings = new UpdateSettings(configuration);
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
