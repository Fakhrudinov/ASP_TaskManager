using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;


namespace MetricsAgentTests
{
    public class UnitTestHDD
    {
        private HddMetricsController controller;

        public UnitTestHDD()
        {
            controller = new HddMetricsController();
        }

        [Fact]
        public void GetMetricsHDDLeftMb_ReturnsOk()
        {
            //Arrange
            var availableMb = 1;

            //Act
            var result = controller.GetMetricsHDDLeftMb(availableMb);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}

