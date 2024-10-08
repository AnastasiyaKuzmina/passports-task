using PassportApplication.Models;
using PassportApplication.Services.Interfaces;
using System.Text.RegularExpressions;

namespace PassportApplication.Services
{
    public class ParserService : IParserService<Passport>
    {
        const char delimeter = ',';
        const string seriesTemplate = @"\d{4}";
        const string numberTemplate = @"\d{6}";

        public Passport? Parse(string input)
        {
            string[] passportFields = input.Split(delimeter);

            if (passportFields.Length != 2 )
            {
                return null;
            }

            if (!CheckPassport(passportFields[0], passportFields[1])) 
            { 
                return null; 
            }

            return new Passport { Series = passportFields[0], Number = passportFields[1] };
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
