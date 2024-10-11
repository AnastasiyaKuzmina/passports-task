using Microsoft.EntityFrameworkCore;
using PassportApplication.Models;
using PassportApplication.Services.Interfaces;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using EFCore.BulkExtensions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
            await Task.Run(() =>
            {
                var databasePassports = _applicationContext.Passports.ToList();
                var deletedPassports = databasePassports.Except(passports).ToList();
                var addedPassports = passports.Except(databasePassports).ToList();

                foreach (var passport in deletedPassports)
                {
                    Remove(passport);
                }

                foreach (var passport in addedPassports)
                {
                    Add(passport);
                }

                _applicationContext.SaveChanges();
            });

            //await Task.Run(() =>
            //{
                //EntityFrameworkManager.PreBulkSaveChanges = ctx =>
                //{
                //    List<PassportChangesHistory> passportChanges = new List<PassportChangesHistory>();
                //    DateOnly date = DateOnly.FromDateTime(DateTime.Now);
                //    ctx.ChangeTracker.DetectChanges();

                //    var added = ctx.ChangeTracker.Entries()
                //    .Where(t => t.State == EntityState.Added)
                //    .Select(t => t.Entity as Passport)
                //    .ToList();

                //    Debug.WriteLine("Added:" + added.Count);

                //    foreach (var entity in added)
                //    {
                //        passportChanges.Add(new PassportChangesHistory { Series = entity.Series, Number = entity.Number, ChangeType = true, Date = date });
                //    }

                //    var deleted = ctx.ChangeTracker.Entries()
                //        .Where(t => t.State == EntityState.Deleted)
                //        .Select(t => t.Entity as Passport)
                //        .ToList();

                //    foreach (var entity in deleted)
                //    {
                //        passportChanges.Add(new PassportChangesHistory { Series = entity.Series, Number = entity.Number, ChangeType = false, Date = date });
                //    }

                //    ctx.BulkInsert(passportChanges);
                //};

            //    _applicationContext.BulkInsertOrUpdateOrDelete(passports);
            //});
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

        //private bool Exist(Passport passport)
        //{
        //    if (_applicationContext.Passports.Any(p => (p.Series == passport.Series) && (p.Number == passport.Number)))
        //    {
        //        return true;
        //    }
        //    return false;
        //}
    }
}
