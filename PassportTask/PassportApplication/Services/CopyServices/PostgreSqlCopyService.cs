using Microsoft.EntityFrameworkCore;
using Npgsql;

using PassportApplication.Database;
using PassportApplication.Services.Interfaces;
using PassportApplication.Results;

namespace PassportApplication.Services.CopyServices
{
    /// <summary>
    /// Implements ICopyService
    /// </summary>
    public class PostgreSqlCopyService : ICopyService
    {
        private readonly ApplicationContext _applicationContext;

        /// <summary>
        /// Constructor of PostgreSqlCopyService
        /// </summary>
        /// <param name="applicationContext">Application context</param>
        public PostgreSqlCopyService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        /// <summary>
        /// Copies from csv to database
        /// </summary>
        /// <param name="FilePath">File path</param>
        /// <returns></returns>
        public async Task<Result> CopyAsync(string FilePath)
        {
            string path = Path.GetFullPath(FilePath);

            using (NpgsqlConnection connection = new NpgsqlConnection(_applicationContext.Database.GetConnectionString()))
            {
                await connection.OpenAsync();
                NpgsqlCommand command1 = new NpgsqlCommand("CREATE TEMP TABLE TempPassports (Series CHAR(4), Number CHAR(6));", connection);
                await command1.ExecuteNonQueryAsync();
                NpgsqlCommand command2 = new NpgsqlCommand(string.Format("COPY TempPassports (Series, Number) FROM \'{0}\' DELIMITER ',' CSV HEADER;", path), connection);
                await command2.ExecuteNonQueryAsync();
                NpgsqlCommand command3 = new NpgsqlCommand("NSERT INTO public.testpassports2 (series, \"number\") \r\nSELECT DISTINCT CAST(Series AS smallint), CAST(substring(Number from 4 for 3) AS integer)\r\nFROM TempPassports WHERE (Series ~ '^\\d{4}$' AND Number ~ '^\\d{6}$')\r\nON CONFLICT (series, \"number\") DO UPDATE SET active = false;", connection);
                await command3.ExecuteNonQueryAsync();
                NpgsqlCommand command4 = new NpgsqlCommand("DROP TABLE TempPassports", connection);
                await command4.ExecuteNonQueryAsync();
                connection.Close();
            }
            return new Result();
        }
    }
}
