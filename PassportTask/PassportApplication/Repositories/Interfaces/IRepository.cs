using PassportApplication.Models.Dto;
using PassportApplication.Results.Generic;

namespace PassportApplication.Repositories.Interfaces
{
    /// <summary>
    /// Repository pattern interface
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Gets passport's current activity status
        /// </summary>
        /// <param name="series">Passport series</param>
        /// <param name="number">Passport number</param>
        /// <returns>Passport's activity status</returns>
        Task<Result<PassportDto>> GetPassportActivityAsync(string series, string number);
        /// <summary>
        /// Gets passport's activity history
        /// </summary>
        /// <param name="series">Passport series</param>
        /// <param name="number">Passport number</param>
        /// <returns>Passport's history</returns>
        Task<Result<List<PassportActivityHistoryDto>>> GetPassportHistoryAsync(string series, string number);
        /// <summary>
        /// Gets passports' changes for date
        /// </summary>
        /// <param name="day">Day</param>
        /// <param name="month">Month</param>
        /// <param name="year">Year</param>
        /// <returns>Passports' changes for date</returns>
        Task<Result<List<PassportChangesDto>>> GetPassportsChangesForDateAsync(short day, short month, short year);
        
    }
}
