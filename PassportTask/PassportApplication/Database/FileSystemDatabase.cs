using System.Diagnostics;
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

        /// <summary>
        /// File system settings
        /// </summary>
        public FileSystemSettings FileSystemSettings { get; set; }

        /// <summary>
        /// Constructor of FileSystemDatabase
        /// </summary>
        /// <param name="fileSystemSettings">File system settings</param>
        public FileSystemDatabase(FileSystemSettings fileSystemSettings) 
        {
            FileSystemSettings = fileSystemSettings;
            EnsureCreated();
        }

        private void EnsureCreated()
        {
            if (Directory.Exists(FileSystemSettings.DatabasePath) == false) 
            {
                Directory.CreateDirectory(FileSystemSettings.DatabasePath);
                Directory.CreateDirectory(FileSystemSettings.PassportsHistoryPath);
                InitializePassportsFile();
                FileSystemSettings.CurrentPassportsPath = true;
                File.Copy(FileSystemSettings.PassportsTemplatePath, FileSystemSettings.PassportsPath1);
                return;
            }

            if (File.Exists(FileSystemSettings.PassportsTemplatePath) == false)
            {
                InitializePassportsFile();
            }

            if (((File.Exists(FileSystemSettings.PassportsPath1) == false) && (File.Exists(FileSystemSettings.PassportsPath2) == false)) ||
                ((File.Exists(FileSystemSettings.PassportsPath1) == true) && (File.Exists(FileSystemSettings.PassportsPath2) == true)) ||
                (Directory.Exists(FileSystemSettings.PassportsHistoryPath) == false))
            {
                throw new Exception();
            }

            if ((File.Exists(FileSystemSettings.PassportsPath1) == false) && (File.Exists(FileSystemSettings.PassportsPath2) == true))
            {
                FileSystemSettings.CurrentPassportsPath = false;
            }
            else
            {
                FileSystemSettings.CurrentPassportsPath = true;
            }
        }

        private void InitializePassportsFile()
        {
            Debug.WriteLine("Start Initialization");
            Stopwatch sw = Stopwatch.StartNew();
            
            using (FileStream fstream = new FileStream(FileSystemSettings.PassportsTemplatePath, FileMode.Create))
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