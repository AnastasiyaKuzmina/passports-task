using Microsoft.EntityFrameworkCore;
using Npgsql;
using PassportApplication.Database;
using PassportApplication.Services.Interfaces;
using System.Diagnostics;

namespace PassportApplication.Services.CopyServices
{
    public class PostgreSqlCopyService : ICopyService
    {
        private readonly ApplicationContext _applicationContext;

        /// <summary>
        /// Constructor of DatabaseService
        /// </summary>
        /// <param name="applicationContext">Application context</param>
        public PostgreSqlCopyService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task CopyAsync(string FilePath)
        {
            string path = Path.GetFullPath(FilePath).Replace("\\", "\\");
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_applicationContext.Database.GetConnectionString()))
                {
                    await connection.OpenAsync();
                    // NpgsqlCommand command = new NpgsqlCommand(string.Format("COPY public.\"Passports\" FROM \'{0}\' DELIMITER ',' CSV HEADER;", path), connection);
                    //NpgsqlCommand command = new NpgsqlCommand("COPY public.\"Passports\" FROM \'C:\\Users\\anast\\OneDrive\\Документы\\passports-task\\PassportTask\\PassportApplication\\Files\\File\\Data.csv\' DELIMITER ',' CSV HEADER;", connection);
                    NpgsqlCommand command = new NpgsqlCommand("COPY public.\"Passports\" FROM \'C:\\pr\\Data.csv\' DELIMITER ',' CSV HEADER;", connection);
                    command.ExecuteNonQuery();
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
