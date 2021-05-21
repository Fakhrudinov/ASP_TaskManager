using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using MetricsManager.DAL;
using MetricsManager;

namespace MetricsManagerTests
{
    public class UnitTestAgentController
    {
        private AgentsController controller;
        private List<AgentInfo> _registeredAgents = new List<AgentInfo>();
        private Mock<ILogger<AgentsController>> logger;

        public UnitTestAgentController()
        {
            _registeredAgents.Add(new AgentInfo());
            logger = new Mock<ILogger<AgentsController>>();
            controller = new AgentsController(_registeredAgents, logger.Object);
        }

        [Fact]
        public void RegisterAgent_ReturnsOk()
        {
            //Arrange
            // ??

            //Act
            var result = controller.RegisterAgent(_registeredAgents[0]);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }


        [Fact]
        public void EnableAgentById_ReturnsOk()
        {
            //Arrange
            // ??

            //Act
            var result = controller.EnableAgentById(_registeredAgents[0].AgentId);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }


        [Fact]
        public void DisableAgentById_ReturnsOk()
        {
            //Arrange
            // ??

            //Act
            var result = controller.DisableAgentById(_registeredAgents[0].AgentId);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
