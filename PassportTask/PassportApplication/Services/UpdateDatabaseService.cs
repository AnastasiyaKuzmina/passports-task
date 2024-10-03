using PassportApplication.Models;
using PassportApplication.Services.Interfaces;
using System.Diagnostics;
using System.IO.Compression;
using System.IO;
using Quartz;

namespace PassportApplication.Services
{
    public class UpdateDatabaseService : IUpdateDatabaseService
    {
        const string DirectoryPath = "./Files"; 
        const string ZipName = "Passports.zip";
        const string ExtractPath = DirectoryPath + "/File/";
        const string FilePath = DirectoryPath + "/" + ZipName;
        const int pageSize = 1000;

        public async Task UpdateDatabase(string url, ApplicationContext applicationContext)
        {
            await LoadFile(url);
            await LoadToDataBase(applicationContext);
        }

        private async Task LoadFile(string url)
        {
            if (!Directory.Exists(DirectoryPath))
            {
                Directory.CreateDirectory(DirectoryPath);
            } 
            else
            {
                Directory.Delete(DirectoryPath, true);
                Directory.CreateDirectory(DirectoryPath);
            }

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
            var files = Directory.GetFiles(ExtractPath);
            int recordsCount = applicationContext.Passports.Count();

            using (var streamReader = new StreamReader(files[0]))
            {
                string? line = await streamReader.ReadLineAsync();
                string[] passportFields;
                Passport? existPassport;

                while ((line = await streamReader.ReadLineAsync()) != null)
                {
                    passportFields = line.Split(",");
                    existPassport = applicationContext.Passports.Find(new string[]{ passportFields[0], passportFields[1] });
                    await applicationContext.AddAsync(new Passport { Series = passportFields[0], Number = passportFields[1] });
                }
            }
        }
    }
}
