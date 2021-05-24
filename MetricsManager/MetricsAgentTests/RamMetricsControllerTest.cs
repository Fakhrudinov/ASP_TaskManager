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
    public class RamMetricsControllerTest
    {
        private RamMetricsController controller;
        private Mock<IRamMetricsRepository> mock;
        private Mock<ILogger<RamMetricsController>> logger;
        private readonly IMapper mapper;

        public RamMetricsControllerTest()
        {
            mock = new Mock<IRamMetricsRepository>();
            logger = new Mock<ILogger<RamMetricsController>>();
            controller = new RamMetricsController(logger.Object, mock.Object, mapper);
        }

        [Fact]
        public void GetFromTimeToTime_Test()
        {
            //Arrange
            DateTimeOffset fromTime = DateTimeOffset.FromUnixTimeSeconds(5);
            DateTimeOffset toTime = DateTimeOffset.FromUnixTimeSeconds(10);
            mock.Setup(a => a.GetFromTimeToTime(5, 10)).Returns(new List<RamMetric>()).Verifiable();

            //Act
            var result = controller.GetFromTimeToTime(fromTime, toTime);
            //Assert
            mock.Verify(repository => repository.GetFromTimeToTime(5, 10), Times.AtMostOnce());
            logger.Verify();
        }

        //[Fact]
        //public void GetMetricsAvaliableMemoryMb_ReturnsOk()
        //{
        //    //Arrange
        //    var availableMb = 1;

        //    //Act
        //    var result = controller.GetMetricsAvaliableMemoryMb(availableMb);

        //    // Assert
        //    _ = Assert.IsAssignableFrom<IActionResult>(result);
        //}
    }
}
