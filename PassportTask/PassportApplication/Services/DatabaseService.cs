using Microsoft.EntityFrameworkCore;

using PassportApplication.Models;
using PassportApplication.Services.Interfaces;


namespace PassportApplication.Services
{
    /// <summary>
    /// Database management service
    /// </summary>
    public class DatabaseService : IDatabaseService
    {
        private readonly ApplicationContext _applicationContext;

        /// <summary>
        /// Constructor of DatabaseService
        /// </summary>
        /// <param name="applicationContext">Application context</param>
        public DatabaseService(ApplicationContext applicationContext) 
        {
            _applicationContext = applicationContext;
        }

        /// <summary>
        /// Updates the database
        /// </summary>
        /// <param name="passports">List of passports</param>
        /// <returns></returns>
        public async Task Update(List<Passport> passports)
        {
            //await Task.Run(() =>
            //{
            //    var databasePassports = _applicationContext.Passports.ToList();
            //    var deletedPassports = databasePassports.Except(passports).ToList();
            //    var addedPassports = passports.Except(databasePassports).ToList();

            //    foreach (var passport in deletedPassports)
            //    {
            //        Remove(passport);
            //    }

            //    foreach (var passport in addedPassports)
            //    {
            //        Add(passport);
            //    }

            //    _applicationContext.SaveChanges();
            //});

            await _applicationContext.BulkSynchronizeAsync(passports);
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

        private void Add(Passport passport)
        {
            _applicationContext.Passports.Add(passport);
            _applicationContext.PassportsChangesHistory.Add(new PassportChangesHistory
            {
                Series = passport.Series,
                Number = passport.Number,
                ChangeType = true,
                Date = DateOnly.FromDateTime(DateTime.Now)
            });
        }
    }
}
