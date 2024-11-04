using System.Data;
using System.Diagnostics;

using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

using PassportApplication.Readers;
using PassportApplication.Services.Interfaces;
using PassportApplication.Database;

namespace PassportApplication.Services.CopyServices
{
    /// <summary>
    /// Implements ICopyService
    /// </summary>
    public class SqlBulkCopyService : ICopyService
    {
        private readonly ApplicationContext _applicationContext;

        /// <summary>
        /// Constructor of SqlBulkCopyService
        /// </summary>
        /// <param name="applicationContext">Application context</param>
        public SqlBulkCopyService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        /// <summary>
        /// Copies from csv to database
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
                    bulkCopy.ColumnMappings.Add(0, 1);
                    bulkCopy.ColumnMappings.Add(1, 2);
                    bulkCopy.BulkCopyTimeout = 0;
                    bulkCopy.BatchSize = 10000;

                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    await bulkCopy.WriteToServerAsync(reader);
                    sw.Stop();
                    Debug.WriteLine("End! {0}", sw.Elapsed.TotalSeconds);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
