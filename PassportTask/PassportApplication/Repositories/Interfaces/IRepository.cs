using PassportApplication.Models.Dto;
using PassportApplication.Results.Generic;

namespace PassportApplication.Repositories.Interfaces
{
    public interface IRepository
    {
        Task<Result<PassportDto>> GetPassportActivityAsync(string series, string number);
        Task<Result<List<PassportActivityHistoryDto>>> GetPassportHistoryAsync(string series, string number);
        Task<Result<List<PassportChangesDto>>> GetPassportsChangesForDateAsync(short day, short month, short year);
        
    }
}
