using System.Text.RegularExpressions;
using System.Diagnostics;
using PassportApplication.Services.Interfaces;
using PassportApplication.Database;

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
        public async Task CopyAsync(string FilePath)
        {
            long symbol;
            int byteNumber, index;
            char[] binaryNumber;
            byte[] bytesToRead = new byte[1];

            Debug.WriteLine("Start Copy!");
            Stopwatch sw = new Stopwatch();
            sw.Start();

            await Task.Run(() =>
            {
                string newFilePath = Path.Combine(_fileSystemDatabase.FileSystemSettings.PassportsHistoryPath, DateTime.Now.ToString("dd-MM-yyyy") + ".txt");
                File.Copy(_fileSystemDatabase.FileSystemSettings.PassportsTemplatePath, newFilePath);
                using (FileStream fstream = new FileStream(newFilePath, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(FilePath))
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
                File.Copy(newFilePath, _fileSystemDatabase.FileSystemSettings.PassportsPath, true);
            });

            sw.Stop();
            Debug.WriteLine("End Copy {0}", sw.Elapsed.TotalSeconds);
        }
    }
}
