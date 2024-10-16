using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using PassportApplication.Services.Interfaces;
using System.Data;
using PassportApplication.Database;

namespace PassportApplication.Services
{
    /// <summary>
    /// Database management service
    /// </summary>
    public class DatabaseService : IDatabaseService
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IDataReader _reader;

        /// <summary>
        /// Constructor of DatabaseService
        /// </summary>
        /// <param name="applicationContext">Application context</param>
        public DatabaseService(ApplicationContext applicationContext, IDataReader reader)
        {
            _applicationContext = applicationContext;
            _reader = reader;
        }

        /// <summary>
        /// Updates the database
        /// </summary>
        /// <returns></returns>
        public async Task UpdateAsync()
        {
            try
            {
                using (var bulkCopy = new SqlBulkCopy(_applicationContext.Database.GetConnectionString(), SqlBulkCopyOptions.TableLock))
                {
                    bulkCopy.DestinationTableName = "[passportsdb].[dbo].[Passports]";
                    bulkCopy.ColumnMappings.Add(0, 0);
                    bulkCopy.ColumnMappings.Add(1, 1);
                    bulkCopy.BulkCopyTimeout = 0;
                    bulkCopy.BatchSize = 5000;

                    await bulkCopy.WriteToServerAsync(_reader);
                }
            }
            catch (Exception ex) 
            { 
                Debug.WriteLine(ex);
            }
            
        }

        //private void Remove(Passport passport)
        //{
        //    _applicationContext.Passports.Remove(passport);
        //    _applicationContext.PassportsChangesHistory.Add(new PassportChangesHistory
        //    {
        //        Series = passport.Series,
        //        Number = passport.Number,
        //        ChangeType = false,
        //        Date = DateOnly.FromDateTime(DateTime.Now)
        //    });
        //}

        //private void Add(Passport passport)
        //{
        //    _applicationContext.Passports.Add(passport);
        //    _applicationContext.PassportsChangesHistory.Add(new PassportChangesHistory
        //    {
        //        Series = passport.Series,
        //        Number = passport.Number,
        //        ChangeType = true,
        //        Date = DateOnly.FromDateTime(DateTime.Now)
        //    });
        //}
    }
}
