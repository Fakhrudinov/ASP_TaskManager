using MetricsManager;
using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Xunit;

namespace MetricsManagerTests
{
    public class UnitTestAgentController
    {
        private AgentsController controller;
        private List<AgentInfo> _registeredAgents = new List<AgentInfo>();

        public UnitTestAgentController()
        {
            _registeredAgents.Add(new AgentInfo());
            controller = new AgentsController(_registeredAgents);
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
