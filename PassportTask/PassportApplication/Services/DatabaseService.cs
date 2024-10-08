using PassportApplication.Models;
using PassportApplication.Services.Interfaces;

namespace PassportApplication.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly ApplicationContext _applicationContext;

        public DatabaseService(ApplicationContext applicationContext) 
        {
            _applicationContext = applicationContext;
        }

        public async Task Analyse(Passport passport)
        {
           if (Exist(passport))
            {

            }
        }

        private bool Exist(Passport passport)
        {
            if (_applicationContext.Passports.Any(p => (p.Series == passport.Series) && (p.Number == passport.Number)))
            {
                return true;
            }
            return false;
        }
    }
}
