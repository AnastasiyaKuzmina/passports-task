using PassportApplication.Database;
using PassportApplication.Models.Dto;
using PassportApplication.Options.FormatOptions;
using PassportApplication.Repositories.Interfaces;
using PassportApplication.Results;
using PassportApplication.Results.Generic;

namespace PassportApplication.Repositories
{
    /// <summary>
    /// Implements IRepository
    /// </summary>
    public class FileSystemRepository : IRepository
    {
        private readonly FormatSettings _formatSettings;
        private readonly FileSystemDatabase _fileSystemDatabase;

        /// <summary>
        /// Constructor of FileSystemRepository
        /// </summary>
        /// <param name="fileSystemDatabase">File system database</param>
        /// <param name="formatSettings">Format settings</param>
        public FileSystemRepository(FormatSettings formatSettings, FileSystemDatabase fileSystemDatabase)
        {
            _formatSettings = formatSettings;
            _fileSystemDatabase = fileSystemDatabase;
        }

        /// <summary>
        /// IRepository.GetPassportActivityAsync implementation
        /// </summary>
        /// <param name="series">Passport series</param>
        /// <param name="number">Passport number</param>
        /// <returns>Passport's activity status</returns>
        public async Task<Result<PassportDto>> GetPassportActivityAsync(string series, string number, CancellationToken cancellationToken)
        {
            string path;
            long symbol;
            int byteNumber, index;
            char[] binaryNumber;
            byte[] bytesToRead = new byte[1];

            if (CheckPassport(series, number) == false)
            {
                return Result.Fail("Wrong passport format");
            }

            symbol = 1000000 * long.Parse(series) + int.Parse(number);
            byteNumber = (int)(symbol / 8);
            index = (int)(symbol % 8);

            path = _fileSystemDatabase.CurrentPassportsPath 
                ? _fileSystemDatabase.PassportsPath1 
                : _fileSystemDatabase.PassportsPath2;

            if (File.Exists(path) == false)
            {
                return Result.Fail("File with passports to read doesn't exist");
            }

            await using (FileStream fstream = new FileStream(path, FileMode.Open))
            {
                fstream.Seek(byteNumber, SeekOrigin.Begin);
                await fstream.ReadAsync(bytesToRead, 0, 1, cancellationToken);
            }

            binaryNumber = Convert.ToString(bytesToRead[0], 2).PadLeft(8, '0').ToCharArray();

            return Result<PassportDto>.Ok(new PassportDto { Active = binaryNumber[0] != '0' });
        }

        /// <summary>
        /// IRepository.GetPassportHistoryAsync implementation
        /// </summary>
        /// <param name="series">Passport series</param>
        /// <param name="number">Passport number</param>
        /// <returns>Passport's history</returns>
        public async Task<Result<List<PassportActivityHistoryDto>>> GetPassportHistoryAsync(string series, string number, CancellationToken cancellationToken)
        {
            string path;
            long symbol;
            int byteNumber, index;
            char[] binaryNumber;
            byte[] bytesToRead = new byte[1];

            if (CheckPassport(series, number) == false)
            {
                return Result.Fail("Wrong passport format");
            }

            symbol = 1000000 * long.Parse(series) + int.Parse(number);
            byteNumber = (int)(symbol / 8);
            index = (int)(symbol % 8);

            List<PassportActivityHistoryDto> result = new List<PassportActivityHistoryDto>();

            List<FileSystemInfo> filesList = new DirectoryInfo(_fileSystemDatabase.PassportsHistoryPath)
                .GetFileSystemInfos()
                .OrderBy(f => f.CreationTime)
                .ToList();

            foreach (FileSystemInfo file in filesList)
            {
                if (File.Exists(path = Path.Combine(_fileSystemDatabase.PassportsHistoryPath, file.Name)) == false)
                {
                    return Result.Fail($"{file.Name} doesn't exist");
                }

                await using (FileStream fstream = new FileStream(path, FileMode.Open))
                {
                    fstream.Seek(byteNumber, SeekOrigin.Begin);
                    await fstream.ReadAsync(bytesToRead, 0, 1, cancellationToken);
                }

                binaryNumber = Convert.ToString(bytesToRead[0], 2).PadLeft(8, '0').ToCharArray();
                result.Add(new PassportActivityHistoryDto 
                { 
                    Date = DateOnly.FromDateTime(file.CreationTime), 
                    Active = binaryNumber[0] != '0' 
                });
            }

            return Result<List<PassportActivityHistoryDto>>.Ok(result);
        }

        /// <summary>
        /// IRepository.GetPassportsChangesForDateAsync implementation
        /// </summary>
        /// <param name="day">Day</param>
        /// <param name="month">Month</param>
        /// <param name="year">Year</param>
        /// <returns>Passports' changes for date</returns>
        public async Task<Result<List<PassportChangesDto>>> GetPassportsChangesForDateAsync(short day, short month, short year, CancellationToken cancellationToken)
        {
            DateOnly date = new DateOnly(year, month, day);
            string filePath = Path.Combine(_fileSystemDatabase.PassportsHistoryPath, 
                                            date.ToString(_fileSystemDatabase.FileNameFormat) + ".txt");

            if (File.Exists(filePath) == false) 
            {
                return Result.Fail($"File {filePath} doesn't exist");
            }

            byte[] bytesToRead1 = new byte[1250000];
            byte[] bytesToRead2 = new byte[1250000];

            List<PassportChangesDto> result = new List<PassportChangesDto>();

            string previousFilePath = Path.Combine(_fileSystemDatabase.PassportsHistoryPath, 
                                                    date.AddDays(-1).ToString(_fileSystemDatabase.FileNameFormat) + ".txt");

            if (File.Exists(previousFilePath) == false)
            {
                previousFilePath = _fileSystemDatabase.PassportsTemplatePath;
            }

            await using (FileStream fstreamCurrent = new FileStream(filePath, FileMode.Open))
            {
                await using (FileStream fstreamPrevious = new FileStream(previousFilePath, FileMode.Open))
                {
                    for (int j = 0; j < 10000; j++)
                    {
                        await fstreamCurrent.ReadAsync(bytesToRead1, 0, bytesToRead1.Length, cancellationToken);
                        await fstreamPrevious.ReadAsync(bytesToRead2, 0, bytesToRead2.Length, cancellationToken);

                        for (int i = 0; i < bytesToRead1.Length; i++)
                        {
                            if (bytesToRead1[i] == bytesToRead2[i])
                            {
                                continue;
                            }

                            AddPassportChanges(bytesToRead1[i], bytesToRead2[i], result, j * 1000 + i);
                        }
                    }
                }
            }

            return Result<List<PassportChangesDto>>.Ok(result);
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

                list.Add(new PassportChangesDto 
                { 
                    Series = series, 
                    Number = number, 
                    ChangeType = binaryNumber1[i] == '1' 
                });
            }
        }
    }
}
