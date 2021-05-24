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
    public class NetWorkMetricsControllerTest
    {
        private NetWorkMetricsController controller;
        private Mock<INetWorkMetricsRepository> mock;
        private Mock<ILogger<NetWorkMetricsController>> logger;
        private readonly IMapper mapper;

        public NetWorkMetricsControllerTest()
        {
            mock = new Mock<INetWorkMetricsRepository>();
            logger = new Mock<ILogger<NetWorkMetricsController>>();
            controller = new NetWorkMetricsController(logger.Object, mock.Object, mapper);
        }

        [Fact]
        public void GetFromTimeToTime_Test()
        {
            //Arrange
            DateTimeOffset fromTime = DateTimeOffset.FromUnixTimeSeconds(5);
            DateTimeOffset toTime = DateTimeOffset.FromUnixTimeSeconds(10);
            mock.Setup(a => a.GetFromTimeToTime(5, 10)).Returns(new List<NetWorkMetric>()).Verifiable();

            //Act
            var result = controller.GetFromTimeToTime(fromTime, toTime);
            //Assert
            mock.Verify(repository => repository.GetFromTimeToTime(5, 10), Times.AtMostOnce());
            logger.Verify();
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