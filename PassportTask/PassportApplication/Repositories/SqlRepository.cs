using Mapster;
using PassportApplication.Database;
using PassportApplication.Models;
using PassportApplication.Models.Dto;
using PassportApplication.Options.FormatOptions;
using PassportApplication.Repositories.Interfaces;

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

        public List<PassportChangesDto> GetPassportsChangesForDate(short day, short month, short year)
        {
            throw new NotImplementedException();
        }

        List<PassportActivityHistoryDto>? IRepository.GetPassportHistory(string series, string number)
        {
            throw new NotImplementedException();
        }
    }
}
