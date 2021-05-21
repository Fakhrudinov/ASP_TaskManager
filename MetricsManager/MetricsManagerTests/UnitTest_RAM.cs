using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using Microsoft.Extensions.Logging;
using Moq;
using AutoMapper;
using MetricsManager.DataAccessLayer.Repository;

namespace MetricsManagerTests
{
    public class RamControllerUnitTests
    {
        private RamMetricsController controller;
        private Mock<IRamMetricsRepository> mock;
        private Mock<ILogger<RamMetricsController>> logger;
        private readonly IMapper mapper;

        public RamControllerUnitTests()
        {
            mock = new Mock<IRamMetricsRepository>();
            logger = new Mock<ILogger<RamMetricsController>>();
            controller = new RamMetricsController(logger.Object, mock.Object, mapper);
        }

        [Fact]
        public void GetMetricsFromAgentIdTimeToTime_ReturnsOk()
        {
            //Arrange
            var agentId = 1;
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(10);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);

            //Act
            var result = controller.GetMetricsFromAgentIdTimeToTime(agentId, fromTime, toTime);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetMetricsFromAllClusterTimeToTime_ReturnsOk()
        {
            //Arrange
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(10);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);

            //Act
            var result = controller.GetMetricsFromAllClusterTimeToTime(fromTime, toTime);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        //[Fact]
        //public void GetMetricsByPercentileFromAgent_ReturnsOk()
        //{
        //    //Arrange
        //    var agentId = 1;
        //    var fromTime = TimeSpan.FromSeconds(0);
        //    var toTime = TimeSpan.FromSeconds(100);
        //    var percentile = MetricsManager.Percentile.P95;

        //    //Act
        //    var result = controller.GetMetricsByPercentileFromAgent(agentId, fromTime, toTime, percentile);

        //    // Assert
        //    _ = Assert.IsAssignableFrom<IActionResult>(result);
        //}


        //[Fact]
        //public void GetMetricsByPercentileFromAllCluster_ReturnsOk()
        //{
        //    //Arrange
        //    var fromTime = TimeSpan.FromSeconds(0);
        //    var toTime = TimeSpan.FromSeconds(100);
        //    var percentile = MetricsManager.Percentile.P95;

        //    //Act
        //    var result = controller.GetMetricsByPercentileFromAllCluster(fromTime, toTime, percentile);

        //    // Assert
        //    _ = Assert.IsAssignableFrom<IActionResult>(result);
        //}
    }
}
