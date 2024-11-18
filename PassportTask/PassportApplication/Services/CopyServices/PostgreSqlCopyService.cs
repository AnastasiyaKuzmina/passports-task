using Microsoft.EntityFrameworkCore;
using Npgsql;

using PassportApplication.Database;
using PassportApplication.Options.FormatOptions;

using PassportApplication.Results;
using PassportApplication.Services.Interfaces;

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
        /// <returns>Result instance</returns>
        public async Task<Result> CopyAsync(string filePath, FormatSettings formatSettings)
        {
            string path = Path.GetFullPath(filePath);

            using (NpgsqlConnection connection = new NpgsqlConnection(_applicationContext.Database.GetConnectionString()))
            {
                var sqlCommand = $@"
                    CREATE TEMP TABLE TempPassports (Series CHAR(4), Number CHAR(6));
                    COPY TempPassports (Series, Number) FROM '{path}' DELIMITER ',' CSV HEADER;
                    INSERT INTO public.testpassports2 (series, ""number"") 
                    SELECT DISTINCT CAST(Series AS smallint), CAST(substring(Number from 4 for 3) AS integer) 
                    FROM TempPassports WHERE (Series ~ '^\d{{4}}$' AND Number ~ '^\d{{6}}$') 
                    ON CONFLICT (series, ""number"") DO UPDATE SET active = false;
                    DROP TABLE TempPassports
                    ";

                await connection.OpenAsync();
                NpgsqlCommand command = new NpgsqlCommand(sqlCommand, connection);
                await command.ExecuteNonQueryAsync();
                connection.Close();
            }
            return Result.Ok();
        }
    }
}
