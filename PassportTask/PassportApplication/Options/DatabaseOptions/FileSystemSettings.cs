namespace PassportApplication.Options.DatabaseOptions
{
    /// <summary>
    /// Implements IDatabaseSettings
    /// </summary>
    public record FileSystemSettings
    {
        /// <summary>
        /// Directory
        /// </summary>
        public string Directory {  get; init; }

        /// <summary>
        /// Database name
        /// </summary>
        public string Database {  get; init; }

        /// <summary>
        /// Current passports path: true if PassportsPath, false if PassportsPath2
        /// </summary>
        public bool CurrentPassportsPath { get; set; } = true;

        /// <summary>
        /// Passports path 1
        /// </summary>
        public string Passports1 { get; init; }

        /// <summary>
        /// Passports path 2
        /// </summary>
        public string Passports2 { get; init; }

        /// <summary>
        /// Passports template path
        /// </summary>
        public string PassportsTemplate { get; init; }

        /// <summary>
        /// Passports history path
        /// </summary>
        public string PassportsHistory { get; init; }

        /// <summary>
        /// File name format
        /// </summary>
        public string FileNameFormat = "dd-MM-yyyy";
    }
}