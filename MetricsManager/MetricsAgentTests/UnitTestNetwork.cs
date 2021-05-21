using MetricsAgent.Controllers;
using MetricsAgent.DAL;
using MetricsAgent;
using Moq;
using System;
using Xunit;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace MetricsAgentTests
{
    public class UnitTestNetWork
    {
        private NetWorkMetricsController controller;
        private Mock<INetWorkMetricsRepository> mock;
        private Mock<ILogger<NetWorkMetricsController>> logger;
        private readonly IMapper mapper;

        public UnitTestNetWork()
        {
            mock = new Mock<INetWorkMetricsRepository>();
            logger = new Mock<ILogger<NetWorkMetricsController>>();
            controller = new NetWorkMetricsController(logger.Object, mock.Object, mapper);
        }

        [Fact]
        public void GetFromTimeToTime_Test()
        {
            // Arrange
            var returnList = new List<NetWorkMetric>();
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
        //public void GetMetricsNetWorkFromTimeToTime_ReturnsOk()
        //{
        //    //Arrange
        //    var fromTime = TimeSpan.FromSeconds(0);
        //    var toTime = TimeSpan.FromSeconds(100);

        //    //Act
        //    var result = controller.GetMetricsNetWorkFromTimeToTime(fromTime, toTime);

        //    // Assert
        //    _ = Assert.IsAssignableFrom<IActionResult>(result);
        //}
    }
}