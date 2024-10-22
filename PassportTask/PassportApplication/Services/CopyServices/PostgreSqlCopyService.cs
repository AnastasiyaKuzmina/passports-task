using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Npgsql;

using PassportApplication.Database;
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
        /// <returns></returns>
        public async Task CopyAsync(string FilePath)
        {
            string path = Path.GetFullPath(FilePath);
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_applicationContext.Database.GetConnectionString()))
                {
                    await connection.OpenAsync();
                    NpgsqlCommand command0 = new NpgsqlCommand("CREATE UNIQUE INDEX passport_index ON public.\"Passports\" (\"Series\", \"Number\")", connection);
                    await command0.ExecuteNonQueryAsync();
                    NpgsqlCommand command1 = new NpgsqlCommand("CREATE TEMP TABLE TempPassports (Id SERIAL PRIMARY KEY, Series VARCHAR(4), Number VARCHAR(6));", connection);
                    await command1.ExecuteNonQueryAsync();
                    NpgsqlCommand command2 = new NpgsqlCommand(string.Format("COPY TempPassports (Series, Number) FROM \'{0}\' DELIMITER ',' CSV HEADER;", path), connection);
                    await command2.ExecuteNonQueryAsync();
                    NpgsqlCommand command3 = new NpgsqlCommand("INSERT INTO public.\"Passports\" (\"Series\", \"Number\") SELECT Series, Number FROM TempPassports WHERE (Series ~ '\\d{4}' AND Number ~ '\\d{6}');", connection);
                    await command3.ExecuteNonQueryAsync();
                    NpgsqlCommand command4 = new NpgsqlCommand("DROP TABLE TempPassports", connection);
                    await command4.ExecuteNonQueryAsync();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            
        }
    }
}
