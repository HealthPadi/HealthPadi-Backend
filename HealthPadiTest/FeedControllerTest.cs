using HealthPadiWebApi.Controllers;
using HealthPadiWebApi.DTOs.Response;
using HealthPadiWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HealthPadiTest
{
    public class FeedControllerTest
    {
        private readonly Mock<IFeedService> _feedService;
        private readonly FeedController _feedController;

        public FeedControllerTest()
        {
            _feedService = new Mock<IFeedService>();
            _feedController = new FeedController(_feedService.Object);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            //Arrange
            _feedService.Setup(repo => repo.GetAllFeedsAsync()).ReturnsAsync(new List<FeedDto>());
            // Act
            var result = _feedController.Get().Result;
            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

    }
}