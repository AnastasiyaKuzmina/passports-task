using System.Diagnostics;
using System.Text.RegularExpressions;

using PassportApplication.Database;
using PassportApplication.Results;
using PassportApplication.Services.Interfaces;

namespace PassportApplication.Services.CopyServices
{
    /// <summary>
    /// Implements ICopyService
    /// </summary>
    public class FileSystemCopyService : ICopyService
    {
        private readonly Regex seriesTemplate = new Regex(@"\d{4}");
        private readonly Regex numberTemplate = new Regex(@"\d{6}");

        private readonly FileSystemDatabase _fileSystemDatabase;

        /// <summary>
        /// Constructor of FileSystemCopyService
        /// </summary>
        /// <param name="fileSystemDatabase">File system database</param>
        public FileSystemCopyService(FileSystemDatabase fileSystemDatabase)
        {
            _fileSystemDatabase = fileSystemDatabase;
        }

        /// <summary>
        /// Copies from csv to database
        /// </summary>
        /// <param name="FilePath">File path</param>
        /// <returns></returns>
        public async Task<Result> CopyAsync(string filePath)
        {
            if (File.Exists(filePath) == false)
            {
                return Result.Fail("File for copy doesn't exist");
            }

            long symbol;
            int byteNumber, index;
            char[] binaryNumber;
            byte[] bytesToRead = new byte[1];
            
            string writeFilePath;

            if (_fileSystemDatabase.FileSystemSettings.CurrentPassportsPath)
            {
                writeFilePath = _fileSystemDatabase.FileSystemSettings.PassportsPath2;
            } 
            else
            {
                writeFilePath = _fileSystemDatabase.FileSystemSettings.PassportsPath1;
            }

            if (File.Exists(_fileSystemDatabase.FileSystemSettings.PassportsTemplatePath) == false)
            {
                return Result.Fail("Passports template file doesn't exist");
            }
            
            File.Copy(_fileSystemDatabase.FileSystemSettings.PassportsTemplatePath, writeFilePath, true);

            await Task.Run(() =>
            {
                using (FileStream fstream = new FileStream(writeFilePath, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        int i = 0;
                        string? line;
                        string[] lines;
                        while ((line = sr.ReadLine()) != null)
                        {
                            i++;
                            if (i % 1000000 == 0)
                            {
                                Debug.WriteLine(i);
                            }
                            lines = line.Split(',');

                            if ((seriesTemplate.IsMatch(lines[0]) == false) || (numberTemplate.IsMatch(lines[1]) == false))
                            {
                                continue;
                            }

                            symbol = 1000000 * long.Parse(lines[0]) + int.Parse(lines[1]);
                            byteNumber = (int)(symbol / 8);
                            index = (int)(symbol % 8);

                            fstream.Seek(byteNumber, SeekOrigin.Begin);
                            fstream.Read(bytesToRead, 0, 1);

                            binaryNumber = Convert.ToString(bytesToRead[0], 2).PadLeft(8, '0').ToCharArray();
                            binaryNumber[index] = '0';

                            fstream.Seek(-1, SeekOrigin.Current);
                            byte[] bytesToWrite = { Convert.ToByte(new String(binaryNumber), 2) };
                            fstream.Write(bytesToWrite, 0, 1);
                        }
                    }
                }

                string newFilePath = Path.Combine(_fileSystemDatabase.FileSystemSettings.PassportsHistoryPath, 
                                                    DateTime.Now.ToString(_fileSystemDatabase.FileSystemSettings.FileNameFormat) + ".txt");
                File.Copy(writeFilePath, newFilePath);

                if (_fileSystemDatabase.FileSystemSettings.CurrentPassportsPath)
                {
                    _fileSystemDatabase.FileSystemSettings.CurrentPassportsPath = false;
                    File.Delete(_fileSystemDatabase.FileSystemSettings.PassportsPath1);
                }
                else
                {
                    _fileSystemDatabase.FileSystemSettings.CurrentPassportsPath = true;
                    File.Delete(_fileSystemDatabase.FileSystemSettings.PassportsPath2);
                }
            });

            return Result.Ok();
        }
    }
}
