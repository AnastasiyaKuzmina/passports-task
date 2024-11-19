using Microsoft.Extensions.Options;
using PassportApplication.Database;
using PassportApplication.Options;
using PassportApplication.Results;
using PassportApplication.Services.Interfaces;

namespace PassportApplication.Services.CopyServices
{
    /// <summary>
    /// Implements ICopyService
    /// </summary>
    public class FileSystemCopyService : ICopyService
    {
        private readonly Settings _settings;
        private readonly FileSystemDatabase _fileSystemDatabase;

        /// <summary>
        /// Constructor of FileSystemCopyService
        /// </summary>
        /// <param name="fileSystemDatabase">File system database</param>
        public FileSystemCopyService(IOptions<Settings> settings, FileSystemDatabase fileSystemDatabase)
        {
            _settings = settings.Value;
            _fileSystemDatabase = fileSystemDatabase;
        }

        /// <summary>
        /// Copies from csv to database
        /// </summary>
        /// <param name="FilePath">File path</param>
        /// <returns>Result instance</returns>
        public async Task<Result> CopyAsync(CancellationToken cancellationToken)
        {
            var extractPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _settings.UpdateSettings.Directory, _settings.UpdateSettings.Extract);
            string filePath = Directory.GetFiles(extractPath)[0];
            if (File.Exists(filePath) == false)
            {
                return Result.Fail("File for copy doesn't exist");
            }

            long symbol;
            int byteNumber, index;
            char[] binaryNumber;
            byte[] bytesToRead = new byte[1];
            
            string writeFilePath;

            if (_fileSystemDatabase.CurrentPassportsPath)
            {
                var passportsPath2 = 
                writeFilePath = _fileSystemDatabase.PassportsPath2;
            } 
            else
            {
                writeFilePath = _fileSystemDatabase.PassportsPath1;
            }

            if (File.Exists(_fileSystemDatabase.PassportsTemplatePath) == false)
            {
                return Result.Fail("Passports template file doesn't exist");
            }
            
            File.Copy(_fileSystemDatabase.PassportsTemplatePath, writeFilePath, true);

            bool canceled = false;

            await Task.Run(() =>
            {
                using (FileStream fstream = new FileStream(writeFilePath, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        string? line;
                        string[] lines;
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (cancellationToken.IsCancellationRequested)
                            {
                                canceled = true;
                                return;
                            }

                            lines = line.Split(',');

                            if ((_settings.FormatSettings.SeriesTemplate.IsMatch(lines[0]) == false) 
                            || (_settings.FormatSettings.NumberTemplate.IsMatch(lines[1]) == false))
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
            });

            if (canceled) return Result.Fail("Copy was canceled");

            string newFilePath = Path.Combine(_fileSystemDatabase.PassportsHistoryPath, 
                                                    DateTime.Now.ToString(_settings.FileSystemSettings.FileNameFormat) + ".txt");
            File.Copy(writeFilePath, newFilePath);

            if (_fileSystemDatabase.CurrentPassportsPath)
            {
                File.Delete(_fileSystemDatabase.PassportsPath1);
            }
            else
            {
                File.Delete(_fileSystemDatabase.PassportsPath2);
            }

            _fileSystemDatabase.CurrentPassportsPath = !_fileSystemDatabase.CurrentPassportsPath;

            return Result.Ok();
        }
    }
}
