﻿using PassportApplication.Options.YandexOptions;

namespace PassportApplication.Options.UpdateOptions
{
    /// <summary>
    /// Update settings class
    /// </summary>
    public record UpdateSettings
    {
        /// <summary>
        /// Yandex settings
        /// </summary>
        public YandexSettings YandexSettings { get; init; }
        /// <summary>
        /// Directory path to download
        /// </summary>
        public string Directory { get; init; }
        /// <summary>
        /// File path to download
        /// </summary>
        public string File { get; init; }
        /// <summary>
        /// Extract path
        /// </summary>
        public string Extract { get; init; }

        ///// <summary>
        ///// Constructor of UpdateSettings
        ///// </summary>
        ///// <param name="configuration">Configuration</param>
        //public UpdateSettings(IConfiguration configuration)
        //{
        //    YandexSettings = new YandexSettings(configuration);
        //    DirectoryPath = GetDirectoryPath(configuration);
        //    FilePath = GetFilePath(configuration);
        //    ExtractPath = GetExtractPath(configuration);
        //}

        //private string GetDirectoryPath(IConfiguration configuration)
        //{
        //    var path = configuration.GetSection("DatabaseUpdate").GetSection("Directory").Value;
        //    if (path == null) throw new NotImplementedException();

        //    return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
        //}

        //private string GetFilePath(IConfiguration configuration)
        //{
        //    var path = configuration.GetSection("DatabaseUpdate").GetSection("ZipFile").Value;
        //    if (path == null) throw new NotImplementedException();

        //    return Path.Combine(DirectoryPath, path);
        //}

        //private string GetExtractPath(IConfiguration configuration)
        //{
        //    var path = configuration.GetSection("DatabaseUpdate").GetSection("ExtractDirectory").Value;
        //    if (path == null) throw new NotImplementedException();

        //    return Path.Combine(DirectoryPath, path);
        //}
    }
}