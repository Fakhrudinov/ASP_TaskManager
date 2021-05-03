using System;
using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MetricsAgentTests
{
    public class UnitTestNetwork
    {
        private NetWorkMetricsController controller;

        public UnitTestNetwork()
        {
            controller = new NetWorkMetricsController();
        }

        [Fact]
        public void GetMetricsNetWorkFromTimeToTime_ReturnsOk()
        {
            //Arrange
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            //Act
            var result = controller.GetMetricsNetWorkFromTimeToTime(fromTime, toTime);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}