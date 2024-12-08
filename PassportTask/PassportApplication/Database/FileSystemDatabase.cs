using Microsoft.Extensions.Options;
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

        private FileSystemSettings _fileSystemSettings;

        /// <summary>
        /// Constructor of FileSystemDatabase
        /// </summary>
        /// <param name="fileSystemSettings">File system settings</param>
        public FileSystemDatabase(IOptions<FileSystemSettings> fileSystemSettings)
        {
            _fileSystemSettings = fileSystemSettings.Value;
            DatabasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _fileSystemSettings.Directory, _fileSystemSettings.Database);
            PassportsHistoryPath = Path.Combine(DatabasePath, _fileSystemSettings.PassportsHistory);
            PassportsTemplatePath = Path.Combine(DatabasePath, _fileSystemSettings.PassportsTemplate);
            PassportsPath1 = Path.Combine(DatabasePath, _fileSystemSettings.Passports1);
            PassportsPath2 = Path.Combine(DatabasePath, _fileSystemSettings.Passports2);
            FileNameFormat = _fileSystemSettings.FileNameFormat;
            EnsureCreated();
        }

        /// <summary>
        /// Database path
        /// </summary>
        public string DatabasePath { get; init; }

        /// <summary>
        /// Passport history path
        /// </summary>
        public string PassportsHistoryPath { get; init; }

        /// <summary>
        /// Current passport path
        /// </summary>
        public bool CurrentPassportsPath { get; set; }

        /// <summary>
        /// Passports template path
        /// </summary>
        public string PassportsTemplatePath { get; init; }

        /// <summary>
        /// First passports path
        /// </summary>
        public string PassportsPath1 { get; init; }

        /// <summary>
        /// Second passports path
        /// </summary>
        public string PassportsPath2 { get; init; }

        /// <summary>
        /// File name format
        /// </summary>
        public string FileNameFormat { get; init; }

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
            using (FileStream fstream = new FileStream(PassportsTemplatePath, FileMode.Create))
            {
                for (long j = 0; j < bytesNumber; j++)
                {
                    fstream.Write(initializeByte, 0, initializeByte.Length);
                }
            }
        }
    }
}