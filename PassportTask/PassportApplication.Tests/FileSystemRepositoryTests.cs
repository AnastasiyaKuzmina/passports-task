using Moq;
using PassportApplication.Database;
using PassportApplication.Models.Dto;
using PassportApplication.Repositories;
using PassportApplication.Options.DatabaseOptions;
using PassportApplication.Options.FormatOptions;

using MicrosoftOptions = Microsoft.Extensions.Options.Options;

namespace PassportApplication.Tests
{
    public class FileSystemRepositoryTests
    {
        FileSystemSettings fileSystemSettings = new FileSystemSettings()
        {
            Directory = "FileSystemDatabase",
            Database = "passportsdb",
            Passports1 = "passports1.txt",
            Passports2 = "passports2.txt",
            PassportsTemplate = "passports_template.txt",
            PassportsHistory = "passportshistory",
            FileNameFormat = "dd-MM-yyyy"
        };

        [Fact]
        public async Task GetPassportActivityEnterInvalidSeriesReturnFail()
        {
            // Arrange
            var mockFormatSettings = new Mock<FormatSettings>();
            var mockFileSystemDatabase = new Mock<FileSystemDatabase>(MicrosoftOptions.Create(fileSystemSettings));
            var fileSystemRepository = new FileSystemRepository(mockFormatSettings.Object, mockFileSystemDatabase.Object);

            var cancellationToken = new CancellationToken();
            string series = "0d00";
            string number = "000000";

            // Act
            var result = await fileSystemRepository.GetPassportActivityAsync(series, number, cancellationToken);

            // Assert
            Assert.Equal("Wrong passport format", result.Error?.Message);
        }

        [Fact]
        public async Task GetPassportActivityEnterInvalidNumberReturnFail()
        {
            // Arrange
            var mockFormatSettings = new Mock<FormatSettings>();
            var mockFileSystemDatabase = new Mock<FileSystemDatabase>(MicrosoftOptions.Create(fileSystemSettings));
            var fileSystemRepository = new FileSystemRepository(mockFormatSettings.Object, mockFileSystemDatabase.Object);

            var cancellationToken = new CancellationToken();
            string series = "0000";
            string number = "000d00";

            // Act
            var result = await fileSystemRepository.GetPassportActivityAsync(series, number, cancellationToken);

            // Assert
            Assert.Equal("Wrong passport format", result.Error?.Message);
        }

        [Fact]
        public async Task GetPassportHistoryEnterInvalidSeriesReturnFail()
        {
            // Arrange
            var mockFormatSettings = new Mock<FormatSettings>();
            var mockFileSystemDatabase = new Mock<FileSystemDatabase>(MicrosoftOptions.Create(fileSystemSettings));
            var fileSystemRepository = new FileSystemRepository(mockFormatSettings.Object, mockFileSystemDatabase.Object);

            var cancellationToken = new CancellationToken();
            string series = "0d00";
            string number = "000000";

            // Act
            var result = await fileSystemRepository.GetPassportHistoryAsync(series, number, cancellationToken);

            // Assert
            Assert.Equal("Wrong passport format", result.Error?.Message);
        }

        [Fact]
        public async Task GetPassportHistoryEnterInvalidNumberReturnFail()
        {
            // Arrange
            var mockFormatSettings = new Mock<FormatSettings>();
            var mockFileSystemDatabase = new Mock<FileSystemDatabase>(MicrosoftOptions.Create(fileSystemSettings));
            var fileSystemRepository = new FileSystemRepository(mockFormatSettings.Object, mockFileSystemDatabase.Object);

            var cancellationToken = new CancellationToken();
            string series = "0000";
            string number = "000d00";

            // Act
            var result = await fileSystemRepository.GetPassportHistoryAsync(series, number, cancellationToken);

            // Assert
            Assert.Equal("Wrong passport format", result.Error?.Message);
        }

        [Fact]
        public async Task GetPassportActivityValidPassportReturnPassportDto()
        {
            // Arrange
            var mockFormatSettings = new Mock<FormatSettings>();
            var mockFileSystemDatabase = new Mock<FileSystemDatabase>(MicrosoftOptions.Create(fileSystemSettings));
            var fileSystemRepository = new FileSystemRepository(mockFormatSettings.Object, mockFileSystemDatabase.Object);

            var cancellationToken = new CancellationToken();
            string series = "0000";
            string number = "000000";

            // Act
            var result = await fileSystemRepository.GetPassportActivityAsync(series, number, cancellationToken);

            // Assert
            Assert.IsType<PassportDto>(result.Value);
        }

        [Fact]
        public async Task GetPassportHistoryValidPassportReturnListOfPassportActivityHistoryDto()
        {
            // Arrange
            var mockFormatSettings = new Mock<FormatSettings>();
            var mockFileSystemDatabase = new Mock<FileSystemDatabase>(MicrosoftOptions.Create(fileSystemSettings));
            var fileSystemRepository = new FileSystemRepository(mockFormatSettings.Object, mockFileSystemDatabase.Object);

            var cancellationToken = new CancellationToken();
            string series = "0000";
            string number = "000000";

            // Act
            var result = await fileSystemRepository.GetPassportHistoryAsync(series, number, cancellationToken);

            // Assert
            Assert.IsType<List<PassportActivityHistoryDto>>(result.Value);
        }

        [Fact]
        public async Task GetPassportsHistoryInvalidDateReturnFail()
        {
            // Arrange
            var mockFormatSettings = new Mock<FormatSettings>();
            var mockFileSystemDatabase = new Mock<FileSystemDatabase>(MicrosoftOptions.Create(fileSystemSettings));
            var fileSystemRepository = new FileSystemRepository(mockFormatSettings.Object, mockFileSystemDatabase.Object);

            var cancellationToken = new CancellationToken();
            short day = 1;
            short month = 2;
            short year = 2003;

            DateOnly date = new DateOnly(year, month, day);
            var databasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileSystemSettings.Directory, fileSystemSettings.Database);
            var passportsHistoryPath = Path.Combine(databasePath, fileSystemSettings.PassportsHistory);
            string filePath = Path.Combine(passportsHistoryPath, date.ToString(fileSystemSettings.FileNameFormat) + ".txt");

            // Act
            var result = await fileSystemRepository.GetPassportsChangesForDateAsync(day, month, year, cancellationToken);

            // Assert
            Assert.Equal($"File {filePath} doesn't exist", result.Error?.Message);
        }
    }
}
