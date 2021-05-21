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
    public class UnitTestDotNet
    {
        private DotNetMetricsController controller;
        private Mock<IDotNetMetricsRepository> mock;
        private Mock<ILogger<DotNetMetricsController>> logger;

        public UnitTestDotNet()
        {
            mock = new Mock<IDotNetMetricsRepository>();
            logger = new Mock<ILogger<DotNetMetricsController>>();
            controller = new DotNetMetricsController(logger.Object, mock.Object);
        }

        [Fact]
        public void GetFromTimeToTime_Test()
        {
            // Arrange
            var returnList = new List<DotNetMetric>();
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
    }
}

        //public UnitTestDotNet()
        //{
        //    controller = new DotNetMetricsController();
        //}

        //[Fact]
        //public void GetMetricsDotNetErrorsCountFromTimeToTime_ReturnsOk()
        //{
        //    //Arrange
        //    var fromTime = TimeSpan.FromSeconds(0);
        //    var toTime = TimeSpan.FromSeconds(100);

        //    //Act
        //    var result = controller.GetMetricsDotNetErrorsCountFromTimeToTime(fromTime, toTime);

        //    // Assert
        //    _ = Assert.IsAssignableFrom<IActionResult>(result);
        //}
