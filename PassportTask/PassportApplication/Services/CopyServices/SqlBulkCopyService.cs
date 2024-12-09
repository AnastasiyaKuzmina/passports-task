using System.Data;
using System.Diagnostics;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Data.SqlClient;

using PassportApplication.Database;
using PassportApplication.Readers;
using PassportApplication.Results;
using PassportApplication.Services.Interfaces;
using PassportApplication.Options.UpdateOptions;

namespace PassportApplication.Services.CopyServices
{
    /// <summary>
    /// Implements ICopyService
    /// </summary>
    public class SqlBulkCopyService : ICopyService
    {
        private readonly UpdateSettings _updateSettings;
        private readonly ApplicationContext _applicationContext;

        /// <summary>
        /// Constructor of SqlBulkCopyService
        /// </summary>
        /// <param name="applicationContext">Application context</param>
        public SqlBulkCopyService(IOptions<UpdateSettings> updateSettings, ApplicationContext applicationContext)
        {
            _updateSettings = updateSettings.Value;
            _applicationContext = applicationContext;
        }

        /// <summary>
        /// Copies from csv to database
        /// </summary>
        /// <returns>Result instance</returns>
        public async Task<Result> CopyAsync(CancellationToken cancellationToken)
        {
            var extractPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _updateSettings.Directory, _updateSettings.Extract);
            string filePath = Directory.GetFiles(extractPath)[0];
            IDataReader reader = new CsvReader(filePath);
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
            return Result.Ok();
        }
    }
}
