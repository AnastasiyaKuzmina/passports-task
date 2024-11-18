namespace PassportApplication.Options.YandexOptions
{
    /// <summary>
    /// Yandex settings class
    /// </summary>
    public class YandexSettings
    {
        /// <summary>
        /// Yandex Disk Token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// Yandex Disk file directory
        /// </summary>
        public string Directory { get; set; }
        /// <summary>
        /// Yandex Disk file name
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Constructor of YandexSettings
        /// </summary>
        /// <param name="configuration">Configuration</param>
        public YandexSettings(IConfiguration configuration) 
        {
            Token = GetToken(configuration);
            Directory = GetDirectory(configuration);
            FileName = GetFileName(configuration);
        }

        private string GetToken(IConfiguration configuration)
        {
            var token = configuration.GetSection("DatabaseUpdate").GetSection("YandexDisk").GetSection("Token").Value;
            if (token == null) throw new NotImplementedException();

            return token;
        }

        private string GetDirectory(IConfiguration configuration)
        {
            var path = configuration.GetSection("DatabaseUpdate").GetSection("YandexDisk").GetSection("Directory").Value;
            if (path == null) throw new NotImplementedException();

            return path;
        }

        private string GetFileName(IConfiguration configuration)
        {
            var path = configuration.GetSection("DatabaseUpdate").GetSection("YandexDisk").GetSection("FileName").Value;
            if (path == null) throw new NotImplementedException();

            return path;
        }
    }
}
