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
    public class UnitTestRam
    {
        private RamMetricsController controller;
        private Mock<IRamMetricsRepository> mock;
        private Mock<ILogger<RamMetricsController>> logger;
        private readonly IMapper mapper;

        public UnitTestRam()
        {
            mock = new Mock<IRamMetricsRepository>();
            logger = new Mock<ILogger<RamMetricsController>>();
            controller = new RamMetricsController(logger.Object, mock.Object, mapper);
        }

        [Fact]
        public void GetFromTimeToTime_Test()
        {
            // Arrange
            var returnList = new List<RamMetric>();
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
