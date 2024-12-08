using PassportApplication.Options.DatabaseOptions;
using PassportApplication.Options.DatabaseOptions.Interfaces;
using PassportApplication.Options.Enums;
using PassportApplication.Options.FormatOptions;
using PassportApplication.Options.UpdateOptions;

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
        /// File system settings
        /// </summary>
        public FileSystemSettings FileSystemSettings { get; init; }

        /// <summary>
        /// MSSQL settings
        /// </summary>
        public MsSqlSettings MsSqlSettings { get; init; }

        /// <summary>
        /// PostgreSQL settings
        /// </summary>
        public PostgreSqlSettings PostgreSqlSettings { get; init; }

        /// <summary>
        /// Update settings
        /// </summary>
        public UpdateSettings UpdateSettings { get; init; }

        /// <summary>
        /// Format settings
        /// </summary>
        public FormatSettings FormatSettings { get; init; } = new FormatSettings();
    }
}
