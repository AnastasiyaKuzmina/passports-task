using PassportApplication.Options.DatabaseOptions;
using PassportApplication.Options.Enums;

namespace PassportApplication.Options
{
    /// <summary>
    /// Settings class
    /// </summary>
    public record Settings
    {
        /// <summary>
        /// Database mode
        /// </summary>
        public DatabaseMode DatabaseMode { get; init; }

        /// <summary>
        /// MSSQL settings
        /// </summary>
        public MsSqlSettings MsSqlSettings { get; init; }

        /// <summary>
        /// PostgreSQL settings
        /// </summary>
        public PostgreSqlSettings PostgreSqlSettings { get; init; }
    }
}
