﻿using PassportApplication.Options.DatabaseOptions;
using System.Diagnostics;

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
                File.Copy(FileSystemSettings.PassportsTemplatePath, FileSystemSettings.PassportsPath);
                return;
            }

            if (File.Exists(FileSystemSettings.PassportsTemplatePath) == false)
            {
                InitializePassportsFile();
            }

            if ((File.Exists(FileSystemSettings.PassportsPath) == false) ||
                (Directory.Exists(FileSystemSettings.PassportsHistoryPath) == false))
            {
                throw new Exception();
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