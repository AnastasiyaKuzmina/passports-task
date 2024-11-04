using System.Text.RegularExpressions;

namespace PassportApplication.Options.FormatOptions
{
    public class FormatSettings
    {
        public Regex SeriesTemplate {  get; }
        public Regex NumberTemplate {  get; }

        public FormatSettings() 
        {
            SeriesTemplate = new Regex(@"\d{4}");
            NumberTemplate = new Regex(@"\d{6}");
        }
    }
}
