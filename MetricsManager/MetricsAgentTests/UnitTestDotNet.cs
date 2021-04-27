using System;
using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MetricsAgentTests
{
    public class UnitTestDotNet
    {
        private DotNetMetricsController controller;

        public UnitTestDotNet()
        {
            controller = new DotNetMetricsController();
        }

        [Fact]
        public void GetMetricsDotNetErrorsCountFromTimeToTime_ReturnsOk()
        {
            //Arrange
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            //Act
            var result = controller.GetMetricsDotNetErrorsCountFromTimeToTime(fromTime, toTime);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}