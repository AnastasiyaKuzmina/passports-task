using PassportApplication.Options.DatabaseOptions.Interfaces;

namespace PassportApplication.Options.DatabaseOptions
{
    /// <summary>
    /// Implements IDatabaseSettings
    /// </summary>
    public record MsSqlSettings
    {
        /// <summary>
        /// Connection string
        /// </summary>
        public string ConnectionString { get; init; }

        ///// <summary>
        ///// Constructor of MsSqlSettings
        ///// </summary>
        ///// <param name="configuration"></param>
        //public MsSqlSettings(IConfiguration configuration)
        //{
        //    ConnectionString = GetConnectionString(configuration);
        //}

        //private string GetConnectionString(IConfiguration configuration)
        //{
        //    var connectionString = configuration.GetConnectionString("SqlConnection");
        //    if (connectionString == null) throw new NotImplementedException();

        //    return connectionString;
        //}
    }
}
