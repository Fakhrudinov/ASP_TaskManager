using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MetricsAgentTests
{
    public class UnitTestRam
    {
        private RamMetricsController controller;

        public UnitTestRam()
        {
            controller = new RamMetricsController();
        }

        [Fact]
        public void GetMetricsAvaliableMemoryMb_ReturnsOk()
        {
            //Arrange
            var availableMb = 1;

            //Act
            var result = controller.GetMetricsAvaliableMemoryMb(availableMb);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
