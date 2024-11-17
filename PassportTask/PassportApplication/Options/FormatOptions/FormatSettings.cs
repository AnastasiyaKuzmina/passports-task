using System.Text.RegularExpressions;

namespace PassportApplication.Options.FormatOptions
{
    public class FormatSettings
    {
        /// <summary>
        /// Series template
        /// </summary>
        public Regex SeriesTemplate {  get; }
        /// <summary>
        /// Number template
        /// </summary>
        public Regex NumberTemplate {  get; }

        /// <summary>
        /// Constructor of FormatSettings
        /// </summary>
        public FormatSettings() 
        {
            SeriesTemplate = new Regex(@"^\d{4}$");
            NumberTemplate = new Regex(@"^\d{6}$");
        }
    }
}
