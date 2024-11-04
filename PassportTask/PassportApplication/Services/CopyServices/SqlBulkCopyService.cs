using System.Data;
using System.Diagnostics;

using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

using PassportApplication.Readers;
using PassportApplication.Services.Interfaces;

namespace PassportApplication.Services.CopyServices
{
    /// <summary>
    /// Implements ICopyService
    /// </summary>
    public class SqlBulkCopyService : ICopyService
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor of SqlBulkCopyService
        /// </summary>
        /// <param name="applicationContext">Application context</param>
        public SqlBulkCopyService(IConfiguration configuration)
        {
            _configuration = configuration;
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
                using (var bulkCopy = new SqlBulkCopy(_configuration.GetConnectionString("SqlConnection"), SqlBulkCopyOptions.TableLock))
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
