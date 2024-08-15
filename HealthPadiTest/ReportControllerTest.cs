using HealthPadiWebApi.Controllers;
using HealthPadiWebApi.DTOs;
using HealthPadiWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HealthPadiTest
{
    public class ReportControllerTest
    {
        private readonly Mock<IReportService> _service;
        private readonly ReportController _controller;
        public ReportControllerTest()
        {
            _service = new Mock<IReportService>();
            _controller = new ReportController(_service.Object);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            //Arrange
            _service.Setup(repo => repo.GetAllReportsAsync(It.IsAny<string>())).ReturnsAsync(new List<ReportDto>());
            // Act
            var result = _controller.Get(null).Result;
            //Assert
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void Get_WhenCalledWithLocation_ReturnsOkResult()
        {
            //Arrange
            _service.Setup(repo => repo.GetAllReportsAsync(It.IsAny<string>())).ReturnsAsync(new List<ReportDto>());
            // Act
            var result = _controller.Get("New York").Result;
            //Assert
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task GetById_UnknownGuidPassed_ReturnsNotFoundResult()
        {
            // Arrange
            var notFoundId = Guid.NewGuid();
            _service.Setup(repo => repo.GetReportByIdAsync(notFoundId)).ReturnsAsync((ReportDto)null);

            // Act
            var result = await _controller.GetById(notFoundId);
            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public async Task GetById_ExistingGuidPassed_ReturnsOkResult()
        {
            // Arrange
            var existingId = Guid.NewGuid();
            var mockReport = new ReportDto { ReportId = existingId, Content = "Lagos Report" };
            _service.Setup(repo => repo.GetReportByIdAsync(existingId)).ReturnsAsync(mockReport);

            // Act
            var result = await _controller.GetById(existingId);
            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Add_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var content = "Report 1";
            var mockReport = new ReportDto { ReportId = Guid.NewGuid(), Content = content };
            var addReportDto = new AddReportDto { Content = content };
            _service.Setup(repo => repo.AddReportAsync(addReportDto)).ReturnsAsync(mockReport);

            // Act
            var createdResponse = await _controller.Add(addReportDto) as CreatedAtActionResult;
            var item = createdResponse.Value as ReportDto;

            // Assert
            Assert.IsType<ReportDto>(item);
            Assert.Equal(content, item.Content);
        }
        [Fact]
        public async Task Delete_UnknownGuidPassed_ReturnsNotFoundResult()
        {
            // Arrange
            var notFoundId = Guid.NewGuid();
            _service.Setup(repo => repo.DeleteReportAsync(notFoundId)).ReturnsAsync((ReportDto)null);

            // Act
            var result = await _controller.Delete(notFoundId);
            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public async Task Delete_ExistingGuidPassed_ReturnsOkResult()
        {
            // Arrange
            var existingId = Guid.NewGuid();
            var mockReport = new ReportDto { ReportId = existingId, Content = "Lagos Report" };
            _service.Setup(repo => repo.DeleteReportAsync(existingId)).ReturnsAsync(mockReport);

            // Act
            var result = await _controller.Delete(existingId);
            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task Delete_ExistingGuidPassed_RemovesOneItem()
        {
            // Arrange
            var existingId = Guid.NewGuid();
            var mockReport = new ReportDto { ReportId = existingId, Content = "Lagos Report" };
            _service.Setup(repo => repo.DeleteReportAsync(existingId)).ReturnsAsync(mockReport);

            // Act
            var result = await _controller.Delete(existingId);
            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

    }
}
