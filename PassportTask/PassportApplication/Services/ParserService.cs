using System.Text.RegularExpressions;

using PassportApplication.Models;
using PassportApplication.Services.Interfaces;

namespace PassportApplication.Services
{
    /// <summary>
    /// Csv parser service
    /// </summary>
    public class ParserService : IParserService
    {
        const char delimeter = ',';
        const string seriesTemplate = @"\d{4}";
        const string numberTemplate = @"\d{6}";

        /// <summary>
        /// Parses csv file
        /// </summary>
        /// <param name="filePath">File path</param>
        /// <returns></returns>
        public async Task<List<Passport>> Parse(string filePath)
        {
            List<Passport> result = new List<Passport>();
            string[] passportFields;

            await Task.Run(() =>
            {
                using (var streamReader = new StreamReader(filePath))
                {
                    string? line = streamReader.ReadLine();

                    while ((line = streamReader.ReadLine()) != null)
                    {
                        passportFields = line.Split(delimeter);

                        if (passportFields.Length != 2)
                        {
                            continue;
                        }

                        if (!CheckPassport(passportFields[0], passportFields[1]))
                        {
                            continue;
                        }

                        result.Add(new Passport { Series = passportFields[0], Number = passportFields[1] });
                    }
                }
            });

            return result;
        }

        private bool CheckPassport(string series, string number)
        {
            Regex seriesRegex = new Regex(seriesTemplate);
            Regex numberRegex = new Regex(numberTemplate);

            if (seriesRegex.IsMatch(series) && numberRegex.IsMatch(number))
            {
                return true;
            }

            return false;
        }
    }
}
