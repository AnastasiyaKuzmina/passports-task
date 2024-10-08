using PassportApplication.Models;

namespace PassportApplication.Services.Interfaces
{
    public interface IDatabaseService
    {
        public Task Analyse(Passport record);
    }
}
