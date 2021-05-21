using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using MetricsManager.DAL;
namespace MetricsManagerTests
{
    public class UnitTestHDD
    {
        private HddMetricsController controller;
        private Mock<IHddMetricsRepository> mock;
        private Mock<ILogger<HddMetricsController>> logger;

        public UnitTestHDD()
        {
            mock = new Mock<IHddMetricsRepository>();
            logger = new Mock<ILogger<HddMetricsController>>();
            controller = new HddMetricsController(logger.Object, mock.Object);
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
