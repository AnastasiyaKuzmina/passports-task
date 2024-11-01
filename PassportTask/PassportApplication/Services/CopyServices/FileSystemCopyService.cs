using System.Text.RegularExpressions;
using PassportApplication.Services.Interfaces;

namespace PassportApplication.Services.CopyServices
{
    public class FileSystemCopyService : ICopyService
    {
        private readonly Regex seriesTemplate = new Regex(@"\d{4}");
        private readonly Regex numberTemplate = new Regex(@"\d{6}");

        private readonly IConfiguration _configuration;

        public FileSystemCopyService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task CopyAsync(string FilePath)
        {
            long symbol;
            int byteNumber, index;
            char[] binaryNumber;
            byte[] bytesToRead = new byte[1];

            string? passportsPath = _configuration.GetSection("PassportsPath").Value;

            if (passportsPath == null)
            {
                return;
            }

            //Stopwatch sw = new Stopwatch();
            //sw.Start();

            await Task.Run(() =>
            {
                using (FileStream fstream = new FileStream(passportsPath, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(FilePath))
                    {
                        string? line;
                        string[] lines;
                        while ((line = sr.ReadLine()) != null)
                        {
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
            });
            
            //sw.Stop();
            //Console.WriteLine(sw.Elapsed.TotalSeconds);
        }
    }
}
