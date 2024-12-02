using System.Diagnostics;
using Microsoft.Extensions.Options;
using PassportApplication.Options;
using PassportApplication.Options.DatabaseOptions;

namespace PassportApplication.Database
{
    /// <summary>
    /// File system database management class
    /// </summary>
    public class FileSystemDatabase
    {
        const long bytesNumber = 1250000000;
        private static readonly byte[] initializeByte = { 255 };

        private readonly FileSystemSettings _fileSystemSettings;

        public string DatabasePath { get; init; }
        public string PassportsHistoryPath { get; init; }
        public bool CurrentPassportsPath { get; set; }
        public string PassportsTemplatePath { get; init; }
        public string PassportsPath1 { get; init; }
        public string PassportsPath2 { get; init; }

        /// <summary>
        /// Constructor of FileSystemDatabase
        /// </summary>
        /// <param name="fileSystemSettings">File system settings</param>
        public FileSystemDatabase(IOptions<Settings> settings) 
        {
            _fileSystemSettings = settings.Value.FileSystemSettings;
            DatabasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, 
                _fileSystemSettings.Directory, _fileSystemSettings.Database);
            PassportsHistoryPath = Path.Combine(DatabasePath, _fileSystemSettings.PassportsHistory);
            PassportsTemplatePath = Path.Combine(DatabasePath, _fileSystemSettings.PassportsTemplate);
            PassportsPath1 = Path.Combine(DatabasePath, _fileSystemSettings.Passports1);
            PassportsPath2 = Path.Combine(DatabasePath, _fileSystemSettings.Passports2);
            EnsureCreated();
        }

        private void EnsureCreated()
        {
            if (Directory.Exists(DatabasePath) == false) 
            {
                Directory.CreateDirectory(DatabasePath);
                Directory.CreateDirectory(PassportsHistoryPath);
                InitializePassportsFile();
                CurrentPassportsPath = true;
                File.Copy(PassportsTemplatePath, PassportsPath1);
                return;
            }

            if (File.Exists(PassportsTemplatePath) == false)
            {
                InitializePassportsFile();
            }

            if (((File.Exists(PassportsPath1) == false) && (File.Exists(PassportsPath2) == false)) ||
                ((File.Exists(PassportsPath1) == true) && (File.Exists(PassportsPath2) == true)) ||
                (Directory.Exists(PassportsHistoryPath) == false))
            {
                throw new Exception();
            }

            if ((File.Exists(PassportsPath1) == false) && (File.Exists(PassportsPath2) == true))
            {
                CurrentPassportsPath = false;
            }
            else
            {
                CurrentPassportsPath = true;
            }
        }

        private void InitializePassportsFile()
        {
            Debug.WriteLine("Start Initialization");
            Stopwatch sw = Stopwatch.StartNew();
            
            using (FileStream fstream = new FileStream(PassportsTemplatePath, FileMode.Create))
            {
                for (long j = 0; j < bytesNumber; j++)
                {
                    fstream.Write(initializeByte, 0, initializeByte.Length);
                }
            }

            sw.Stop();
            Debug.WriteLine("End Initialization {0}", sw.Elapsed.TotalSeconds);
        }
    }
}