using PassportApplication.Models.Dto;

namespace PassportApplication.Repositories.Interfaces
{
    public interface IRepository
    {
        PassportDto? GetPassportActivity(string series, string number);
        List<PassportChangesDto>? GetPassportsChangesForDate(short day, short month, short year);
        List<PassportActivityHistoryDto>? GetPassportHistory(string series, string number);

    }
}
