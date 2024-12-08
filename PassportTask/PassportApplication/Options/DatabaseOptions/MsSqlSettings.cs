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
    }
}
