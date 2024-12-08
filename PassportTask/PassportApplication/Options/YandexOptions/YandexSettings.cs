namespace PassportApplication.Options.YandexOptions
{
    /// <summary>
    /// Yandex settings class
    /// </summary>
    public record YandexSettings
    {
        /// <summary>
        /// Yandex Disk Token
        /// </summary>
        public string Token { get; init; }
        /// <summary>
        /// Yandex Disk file directory
        /// </summary>
        public string Directory { get; init; }
        /// <summary>
        /// Yandex Disk file name
        /// </summary>
        public string FileName { get; init; }
    }
}
