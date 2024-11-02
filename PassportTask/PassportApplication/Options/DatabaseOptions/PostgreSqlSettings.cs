using PassportApplication.Options.DatabaseOptions.Interfaces;

namespace PassportApplication.Options.DatabaseOptions
{
    public class PostgreSqlSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; }

        public PostgreSqlSettings(IConfiguration configuration)
        {
            ConnectionString = GetConnectionString(configuration);
        }

        private string GetConnectionString(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("NpgSqlConnection");
            if (connectionString == null) throw new NotImplementedException();

            return connectionString;
        }
    }
}
