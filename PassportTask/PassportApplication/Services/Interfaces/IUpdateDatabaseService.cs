using PassportApplication.Models;

namespace PassportApplication.Services.Interfaces
{
    public interface IUpdateDatabaseService
    {
        public Task UpdateDatabase(string url, ApplicationContext applicationContext);
    }
}
