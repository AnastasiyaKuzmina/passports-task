using Mapster;

using PassportApplication.Database;
using PassportApplication.Models;
using PassportApplication.Models.Dto;
using PassportApplication.Options.FormatOptions;
using PassportApplication.Repositories.Interfaces;
using PassportApplication.Results.Generic;

namespace PassportApplication.Repositories
{
    /// <summary>
    /// Implements IRepository
    /// </summary>
    public class SqlRepository : IRepository
    {
        private readonly ApplicationContext _applicationContext;
        private readonly FormatSettings _formatSettings;

        /// <summary>
        /// Constructor of SqlRepository
        /// </summary>
        /// <param name="applicationContext">Application context</param>
        /// <param name="formatSettings">Format settings</param>
        public SqlRepository(ApplicationContext applicationContext, FormatSettings formatSettings)
        {
            _applicationContext = applicationContext;
            _formatSettings = formatSettings;
        }

        /// <summary>
        /// IRepository.GetPassportActivityAsync implementation
        /// </summary>
        /// <param name="series">Passport series</param>
        /// <param name="number">Passport number</param>
        /// <returns>Passport's activity status</returns>
        public async Task<Result<PassportDto>> GetPassportActivityAsync(string series, string number, CancellationToken cancellationToken)
        {
            if ((_formatSettings.SeriesTemplate.IsMatch(series) == false) 
                || (_formatSettings.NumberTemplate.IsMatch(number) == false))
            {
                return Result<PassportDto>.Fail("Wrong passport format");
            }

            Passport? passport = await _applicationContext.Passports.FindAsync(short.Parse(series), int.Parse(number));
            return Result<PassportDto>.Ok(passport.Adapt<PassportDto>());
        }

        /// <summary>
        /// IRepository.GetPassportHistoryAsync implementation
        /// </summary>
        /// <param name="series">Passport series</param>
        /// <param name="number">Passport number</param>
        /// <returns>Passport's history</returns>
        public Task<Result<List<PassportActivityHistoryDto>>> GetPassportHistoryAsync(string series, string number, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// IRepository.GetPassportsChangesForDateAsync implementation
        /// </summary>
        /// <param name="day">Day</param>
        /// <param name="month">Month</param>
        /// <param name="year">Year</param>
        /// <returns>Passports' changes for date</returns>
        public Task<Result<List<PassportChangesDto>>> GetPassportsChangesForDateAsync(short day, short month, short year, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        
    }
}
