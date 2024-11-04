using Mapster;
using PassportApplication.Database;
using PassportApplication.Models;
using PassportApplication.Options.FormatOptions;
using PassportApplication.Repositories.Interfaces;
using System.Text.RegularExpressions;

namespace PassportApplication.Repositories
{
    public class SqlRepository : IRepository
    {
        private readonly ApplicationContext _applicationContext;
        private readonly FormatSettings _formatSettings;

        public SqlRepository(ApplicationContext applicationContext, FormatSettings formatSettings)
        {
            _applicationContext = applicationContext;
            _formatSettings = formatSettings;
        }

        public PassportDto? GetPassportActivity(string series, string number)
        {
            if ((_formatSettings.SeriesTemplate.IsMatch(series) == false) 
                || (_formatSettings.NumberTemplate.IsMatch(number) == false))
            {
                return null;
            }

            Passport? passport = _applicationContext.Passports.Find(short.Parse(series), int.Parse(number));
            return passport.Adapt<PassportDto>();
        }
    }
}
