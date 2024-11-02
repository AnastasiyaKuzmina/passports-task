using PassportApplication.Options.DatabaseOptions.Interfaces;

namespace PassportApplication.Options.DatabaseOptions
{
    public class MsSqlSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; }

        public MsSqlSettings(IConfiguration configuration)
        {
            ConnectionString = GetConnectionString(configuration);
        }

        private string GetConnectionString(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlConnection");
            if (connectionString == null) throw new NotImplementedException();

            return connectionString;
        }
    }
}
