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
    }
}