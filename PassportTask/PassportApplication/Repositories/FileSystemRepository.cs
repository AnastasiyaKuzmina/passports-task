using PassportApplication.Database;
using PassportApplication.Models.Dto;
using PassportApplication.Options.FormatOptions;
using PassportApplication.Repositories.Interfaces;
using PassportApplication.Results;
using PassportApplication.Errors;
using PassportApplication.Errors.Enums;

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
        
        public async Task<Result<PassportDto>> GetPassportActivityAsync(string series, string number)
        {
            string path;
            long symbol;
            int byteNumber, index;
            char[] binaryNumber;
            byte[] bytesToRead = new byte[1];

            if (CheckPassport(series, number) == false)
            {
                return new Result<PassportDto>(new Error(ErrorType.WrongPassportFormat, "Wrong passport format"));
            }

            symbol = 1000000 * long.Parse(series) + int.Parse(number);
            byteNumber = (int)(symbol / 8);
            index = (int)(symbol % 8);

            if (_fileSystemDatabase.FileSystemSettings.CurrentPassportsPath)
            {
                path = _fileSystemDatabase.FileSystemSettings.PassportsPath1;
            }
            else
            {
                path = _fileSystemDatabase.FileSystemSettings.PassportsPath2;
            }

            if (File.Exists(path) == false)
            {
                return new Result<PassportDto>(new Error(ErrorType.FileDoesNotExist, "File with passports to read doesn't exist"));
            }

            using (FileStream fstream = new FileStream(path, FileMode.Open))
            {
                fstream.Seek(byteNumber, SeekOrigin.Begin);
                await fstream.ReadAsync(bytesToRead, 0, 1);
            }

            binaryNumber = Convert.ToString(bytesToRead[0], 2).PadLeft(8, '0').ToCharArray();

            return binaryNumber[0] == '0' ? new PassportDto { Active = false } : new PassportDto { Active = true };
        }

        public async Task<Result<List<PassportActivityHistoryDto>>> GetPassportHistoryAsync(string series, string number)
        {
            string path;
            long symbol;
            int byteNumber, index;
            char[] binaryNumber;
            byte[] bytesToRead = new byte[1];

            if (CheckPassport(series, number) == false)
            {
                return new Result<List<PassportActivityHistoryDto>>(new Error(ErrorType.WrongPassportFormat, "Wrong passport format"));
            }

            symbol = 1000000 * long.Parse(series) + int.Parse(number);
            byteNumber = (int)(symbol / 8);
            index = (int)(symbol % 8);

            List<PassportActivityHistoryDto> result = new List<PassportActivityHistoryDto>();

            List<FileSystemInfo> filesList = new DirectoryInfo(_fileSystemDatabase.FileSystemSettings.PassportsHistoryPath).GetFileSystemInfos().OrderBy(f => f.CreationTime).ToList();

            foreach (FileSystemInfo file in filesList)
            {
                if (File.Exists(path = Path.Combine(_fileSystemDatabase.FileSystemSettings.PassportsHistoryPath, file.Name)) == false)
                {
                    return new Result<List<PassportActivityHistoryDto>>(new Error(ErrorType.FileDoesNotExist, $"File {file.Name} doesn't exist"));
                }

                using (FileStream fstream = new FileStream(path, FileMode.Open))
                {
                    fstream.Seek(byteNumber, SeekOrigin.Begin);
                    await fstream.ReadAsync(bytesToRead, 0, 1);
                }

                binaryNumber = Convert.ToString(bytesToRead[0], 2).PadLeft(8, '0').ToCharArray();
                result.Add(binaryNumber[0] == '0' ? new PassportActivityHistoryDto { Date = DateOnly.FromDateTime(file.CreationTime), Active = false }
                                                : new PassportActivityHistoryDto { Date = DateOnly.FromDateTime(file.CreationTime), Active = true });
            }

            return result;
        }

        public async Task<Result<List<PassportChangesDto>>> GetPassportsChangesForDateAsync(short day, short month, short year)
        {
            DateOnly date = new DateOnly(year, month, day);
            string filePath = Path.Combine(_fileSystemDatabase.FileSystemSettings.PassportsHistoryPath, 
                                            date.ToString(_fileSystemDatabase.FileSystemSettings.FileNameFormat) + ".txt");

            if (File.Exists(filePath) == false) 
            {
                return new Result<List<PassportChangesDto>>(new Error(ErrorType.FileDoesNotExist, $"File {filePath} doesn't exist"));
            }

            byte[] bytesToRead1 = new byte[1250000];
            byte[] bytesToRead2 = new byte[1250000];

            List<PassportChangesDto> result = new List<PassportChangesDto>();

            string previousFilePath = Path.Combine(_fileSystemDatabase.FileSystemSettings.PassportsHistoryPath, 
                                                    date.AddDays(-1).ToString(_fileSystemDatabase.FileSystemSettings.FileNameFormat) + ".txt");

            if (File.Exists(previousFilePath) == false)
            {
                previousFilePath = _fileSystemDatabase.FileSystemSettings.PassportsTemplatePath;
            }

            using (FileStream fstreamCurrent = new FileStream(filePath, FileMode.Open))
            {
                using (FileStream fstreamPrevious = new FileStream(previousFilePath, FileMode.Open))
                {
                    for (int j = 0; j < 10000; j++)
                    {
                        await fstreamCurrent.ReadAsync(bytesToRead1, 0, bytesToRead1.Length);
                        await fstreamPrevious.ReadAsync(bytesToRead2, 0, bytesToRead2.Length);

                        for (int i = 0; i < bytesToRead1.Length; i++)
                        {
                            if (bytesToRead1[i] == bytesToRead2[i])
                            {
                                continue;
                            }

                            AddPassportChanges(bytesToRead1[i], bytesToRead2[i], result, j * 1000 + i);
                        }
                    }


                    //while ((fstreamCurrent.Read(bytesToRead1, 0, bytesToRead1.Length) > 0)
                    //    && (fstreamPrevious.Read(bytesToRead2, 0, bytesToRead2.Length) > 0))
                    //{
                    //    if (bytesToRead1[0] == bytesToRead2[0])
                    //    {
                    //        byteNumber++;
                    //        continue;
                    //    }

                    //    AddPassportChanges(bytesToRead1, bytesToRead2, result, byteNumber);
                    //    byteNumber++;
                    //}
                }
            }

            return result;
        }

        private bool CheckPassport(string series, string number)
        {
            if ((_formatSettings.SeriesTemplate.IsMatch(series) == false)
                || (_formatSettings.NumberTemplate.IsMatch(number) == false))
            {
                return false;
            }
            return true;
        }

        private void AddPassportChanges(byte bytesToRead1, byte bytesToRead2, List<PassportChangesDto> list, int byteNumber)
        {
            long symbol;
            int number;
            short series;

            char[] binaryNumber1 = Convert.ToString(bytesToRead1, 2).PadLeft(8, '0').ToCharArray();
            char[] binaryNumber2 = Convert.ToString(bytesToRead2, 2).PadLeft(8, '0').ToCharArray();

            for (int i = 0; i < 8; i++)
            {
                if (binaryNumber1[i] == binaryNumber2[i])
                {
                    continue;
                }

                symbol = byteNumber * 8 + i;
                series = (short)(symbol / 1000000);
                number = (int)(symbol % 1000000);

                if (binaryNumber1[i] == '1')
                {
                    list.Add(new PassportChangesDto { Series = series, Number = number, ChangeType = true });
                    continue;
                }

                list.Add(new PassportChangesDto { Series = series, Number = number, ChangeType = false });
            }
        }
    }
}
