namespace PassportApplication.Options.DatabaseOptions
{
    /// <summary>
    /// Implements IDatabaseSettings
    /// </summary>
    public record PostgreSqlSettings
    {
        /// <summary>
        /// Connection string
        /// </summary>
        public string ConnectionString { get; init; }
    }
}
