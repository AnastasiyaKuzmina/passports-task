using PassportApplication.Options.DatabaseOptions.Interfaces;

namespace PassportApplication.Options.DatabaseOptions
{
    /// <summary>
    /// Implements IDatabaseSettings
    /// </summary>
    public class PostgreSqlSettings : IDatabaseSettings
    {
        /// <summary>
        /// Connection string
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Constructor of PostgreSqlSettings
        /// </summary>
        /// <param name="configuration"></param>
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
