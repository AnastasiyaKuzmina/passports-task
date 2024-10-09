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

        public async Task Update(List<Passport> passports)
        {
            var databasePassports = _applicationContext.Passports;
            var deletedPassports = databasePassports.Except(passports);
            var addedPassports = passports.Except(databasePassports);

            foreach (var passport in deletedPassports)
            {
                Remove(passport);
            }

           if (Exist(passport))
            {

            }
        }

        private void Remove(Passport passport)
        {
            _applicationContext.Passports.Remove(passport);
            _applicationContext.PassportsChangesHistory.Add(new PassportChangesHistory 
            { 
                Series = passport.Series, 
                Number = passport.Number, 
                ChangeType = false, 
                Date = DateOnly.FromDateTime(DateTime.Now) 
            });
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
