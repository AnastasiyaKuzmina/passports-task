using PassportApplication.Models;

namespace PassportApplication.Repositories.Interfaces
{
    public interface IRepository
    {
        PassportDto? GetPassportActivity(string series, string number);
    }
}
