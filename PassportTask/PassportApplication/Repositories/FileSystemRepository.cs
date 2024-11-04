using PassportApplication.Database;
using PassportApplication.Models;
using PassportApplication.Options.FormatOptions;
using PassportApplication.Repositories.Interfaces;

namespace PassportApplication.Repositories
{
    public class FileSystemRepository : IRepository
    {
        private readonly FileSystemDatabase _fileSystemDatabase;
        private readonly FormatSettings _formatSettings;

        public FileSystemRepository(FileSystemDatabase fileSystemDatabase, FormatSettings formatSettings)
        {
            _fileSystemDatabase = fileSystemDatabase;
            _formatSettings = formatSettings;
        }

        public PassportDto? GetPassportActivity(string series, string number)
        {
            long symbol;
            int byteNumber, index;
            char[] binaryNumber;
            byte[] bytesToRead = new byte[1];

            if ((_formatSettings.SeriesTemplate.IsMatch(series) == false) 
                || (_formatSettings.NumberTemplate.IsMatch(number) == false))
            {
                return null;
            }

            symbol = 1000000 * long.Parse(series) + int.Parse(number);
            byteNumber = (int)(symbol / 8);
            index = (int)(symbol % 8);

            using (FileStream fstream = new FileStream(_fileSystemDatabase.FileSystemSettings.PassportsPath, FileMode.Open))
            {
                fstream.Seek(byteNumber, SeekOrigin.Begin);
                fstream.Read(bytesToRead, 0, 1);
            }

            binaryNumber = Convert.ToString(bytesToRead[0], 2).PadLeft(8, '0').ToCharArray();

            return binaryNumber[0] == '0' ? new PassportDto { Active = false } : new PassportDto { Active = true };
        }
    }
}
