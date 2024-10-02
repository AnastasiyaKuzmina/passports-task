using PassportApplication.Models;
using PassportApplication.Services.Interfaces;
using System.Diagnostics;
using System.IO.Compression;
using Quartz;

namespace PassportApplication.Services
{
    public class UpdateDatabaseService : IUpdateDatabaseService
    {
        const string FilePath = "C://Files/Passports.zip";
        const string ExtractPath = "C://Files/";

        public async Task UpdateDatabase(string url, ApplicationContext applicationContext)
        {
            await LoadFile(url);
            await LoadToDataBase(applicationContext);
        }

        private async Task LoadFile(string url)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetStreamAsync(url))
                {
                    using (var fileStream = new FileStream(FilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        await response.CopyToAsync(fileStream);
                        Debug.WriteLine("Download ZIP done!");
                    }
                }
            }

            ZipFile.ExtractToDirectory(FilePath, ExtractPath);
            Debug.WriteLine("Extract ZIP done!");
        }

        private async Task LoadToDataBase(ApplicationContext applicationContext)
        {

        }
    }
}
