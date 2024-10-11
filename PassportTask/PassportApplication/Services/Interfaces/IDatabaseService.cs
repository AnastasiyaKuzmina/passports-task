using PassportApplication.Models;

namespace PassportApplication.Services.Interfaces
{
    public interface IDatabaseService
    {
        public Task Update(List<Passport> passports);
    }
}
