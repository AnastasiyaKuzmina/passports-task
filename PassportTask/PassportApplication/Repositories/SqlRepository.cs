using Mapster;

using PassportApplication.Database;
using PassportApplication.Models;
using PassportApplication.Models.Dto;
using PassportApplication.Options.FormatOptions;
using PassportApplication.Repositories.Interfaces;
using PassportApplication.Results.Generic;

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

        public async Task<Result<PassportDto>> GetPassportActivityAsync(string series, string number)
        {
            if ((_formatSettings.SeriesTemplate.IsMatch(series) == false) 
                || (_formatSettings.NumberTemplate.IsMatch(number) == false))
            {
                return Result<PassportDto>.Fail("Wrong passport format");
            }

            Passport? passport = await _applicationContext.Passports.FindAsync(short.Parse(series), int.Parse(number));
            return Result<PassportDto>.Ok(passport.Adapt<PassportDto>());
        }

        public Task<Result<List<PassportActivityHistoryDto>>> GetPassportHistoryAsync(string series, string number)
        {
            throw new NotImplementedException();
        }

        public Task<Result<List<PassportChangesDto>>> GetPassportsChangesForDateAsync(short day, short month, short year)
        {
            throw new NotImplementedException();
        }

        
    }
}
