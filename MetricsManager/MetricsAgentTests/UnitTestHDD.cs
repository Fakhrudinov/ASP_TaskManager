using MetricsAgent.Controllers;
using MetricsAgent.DAL;
using MetricsAgent;
using Moq;
using System;
using Xunit;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgentTests
{
    public class UnitTestHdd
    {
        private HddMetricsController controller;
        private Mock<IHddMetricsRepository> mock;
        private Mock<ILogger<HddMetricsController>> logger;

        public UnitTestHdd()
        {
            mock = new Mock<IHddMetricsRepository>();
            logger = new Mock<ILogger<HddMetricsController>>();
            controller = new HddMetricsController(logger.Object, mock.Object);
        }

        [Fact]
        public void GetFromTimeToTime_Test()
        {
            // Arrange
            var returnList = new List<HddMetric>();
            mock.Setup(repository => repository.GetFromTimeToTime(
                It.IsAny<DateTimeOffset>().ToUnixTimeSeconds(),
                It.IsAny<DateTimeOffset>().ToUnixTimeSeconds()))
                .Returns(returnList);

            // Act
            IActionResult result = controller.GetFromTimeToTime(
                DateTimeOffset.FromUnixTimeSeconds(10).ToUniversalTime(),
                DateTimeOffset.FromUnixTimeSeconds(100).ToUniversalTime());

            // Assert
            mock.Verify(repository => repository.GetFromTimeToTime(10, 100), Times.AtLeastOnce());
        }

        //[Fact]
        //public void GetMetricsHDDLeftMb_ReturnsOk()
        //{
        //    //Arrange
        //    var availableMb = 1;

        //    //Act
        //    var result = controller.GetMetricsHDDLeftMb(availableMb);

        //    // Assert
        //    _ = Assert.IsAssignableFrom<IActionResult>(result);
        //}
    }
}

