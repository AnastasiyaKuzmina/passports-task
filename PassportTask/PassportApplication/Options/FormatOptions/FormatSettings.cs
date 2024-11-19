using System.Text.RegularExpressions;

namespace PassportApplication.Options.FormatOptions
{
    public record FormatSettings
    {
        /// <summary>
        /// Series template
        /// </summary>
        public Regex SeriesTemplate { get; init; } = new(@"^\d{4}$");
        /// <summary>
        /// Number template
        /// </summary>
        public Regex NumberTemplate { get; init; } = new(@"^\d{6}$");
    }
}
