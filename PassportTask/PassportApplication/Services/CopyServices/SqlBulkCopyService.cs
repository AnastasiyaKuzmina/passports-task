using System.Data;
using System.Diagnostics;

using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

using PassportApplication.Database;
using PassportApplication.Readers;
using PassportApplication.Services.Interfaces;

namespace PassportApplication.Services.CopyServices
{
    /// <summary>
    /// Implements IDatabaseService
    /// </summary>
    public class SqlBulkCopyService : ICopyService
    {
        private readonly ApplicationContext _applicationContext;

        /// <summary>
        /// Constructor of DatabaseService
        /// </summary>
        /// <param name="applicationContext">Application context</param>
        public SqlBulkCopyService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        /// <summary>
        /// Updates the database
        /// </summary>
        /// <returns></returns>
        public async Task CopyAsync(string FilePath)
        {
            IDataReader reader = new CsvReader(FilePath);
            try
            {
                using (var bulkCopy = new SqlBulkCopy(_applicationContext.Database.GetConnectionString(), SqlBulkCopyOptions.TableLock))
                {
                    bulkCopy.DestinationTableName = "[passportsdb].[dbo].[Passports]";
                    bulkCopy.ColumnMappings.Add(0, 0);
                    bulkCopy.ColumnMappings.Add(1, 1);
                    bulkCopy.BulkCopyTimeout = 0;
                    bulkCopy.BatchSize = 10000;

                    await bulkCopy.WriteToServerAsync(reader);
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
