using Microsoft.AspNetCore.Mvc;
using Moq;
using PassportApplication.Controllers;
using PassportApplication.Repositories.Interfaces;

namespace PassportApplication.Tests
{
    public class PassportsControllerTests
    {
        [Fact]
        public async Task GetPassportActivityEmptyEnterReturnsBadRequest()
        {
            // Arrange
            var mockRepository = new Mock<IRepository>();
            var cancellationToken = new CancellationToken();
            string? series = null;
            string? number = null;

            //mockRepository
            //    .Setup(r => r.GetPassportActivityAsync(series, number, cancellationToken))
            //    .ReturnsAsync(Result<PassportDto>.Ok(new PassportDto { Active = true }));

            var controller = new PassportsController(mockRepository.Object);

            // Act
            var result = await controller.GetPassportActivity(series, number, cancellationToken);

            // Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task GetPassportHistoryEmptyEnterReturnsBadRequest()
        {
            // Arrange
            var mockRepository = new Mock<IRepository>();
            var cancellationToken = new CancellationToken();
            string? series = null;
            string? number = null;

            var controller = new PassportsController(mockRepository.Object);

            // Act
            var result = await controller.GetPassportHistory(series, number, cancellationToken);

            // Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task GetPassportsChangesEmptyDateReturnsBadRequest()
        {
            // Arrange
            var mockRepository = new Mock<IRepository>();
            var cancellationToken = new CancellationToken();
            short? day = null;
            short? month = null;
            short? year = null;

            var controller = new PassportsController(mockRepository.Object);

            // Act
            var result = await controller.GetPassportsChangesForDate(day, month, year, cancellationToken);

            // Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }
    }
}