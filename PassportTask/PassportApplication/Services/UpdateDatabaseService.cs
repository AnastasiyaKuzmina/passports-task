using PassportApplication.Models;
using PassportApplication.Services.Interfaces;
using Quartz;

namespace PassportApplication.Services
{
    public class UpdateDatabaseService : IUpdateDatabaseService
    {
        const string FilePath = "/Files/Passports.csv";
        ApplicationContext _context;
        string _fileUrl;
        IWebHostEnvironment _appEnvironment;

        public async Task UpdateDatabase(string url)
        {

        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                await LoadFile();
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private async Task LoadFile()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_fileUrl))
                {
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    {
                        using (var fileStream = new FileStream(FILE_PATH, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            await stream.CopyToAsync(fileStream);
                        }
                    }
                }
            }
        }
    }
}
