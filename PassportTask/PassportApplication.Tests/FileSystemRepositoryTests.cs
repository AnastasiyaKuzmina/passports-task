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
            PassportsHistory = "passportshistory"
        };

        [Fact]
        public async Task GetPassportActivityEnterInvalidSeriesSymbolsReturnFail()
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
    }
}
