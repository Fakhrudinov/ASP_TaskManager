using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using MetricsManager.DTO;
using MetricsManager.DataAccessLayer.Repository;

namespace MetricsManagerTests
{
    public class UnitTestAgentController
    {
        private AgentsController _controller;
        private Mock<ILogger<AgentsController>> _logger;
        private Mock<IAgentsRepository> _repository;


        public UnitTestAgentController()
        {
            _logger = new Mock<ILogger<AgentsController>>();
            _repository = new Mock<IAgentsRepository>();
            _controller = new AgentsController(_logger.Object, _repository.Object);
        }

        [Fact]
        public void RegisterAgent_ReturnsOk()
        {
            _repository.Setup(repo => repo.RegisterAgent(new Agent())).Verifiable();
            var result = _controller.RegisterAgent(new Agent());

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void RemoveAgent_ReturnsOk()
        {
            _repository.Setup(repo => repo.RemoveAgent(new Agent())).Verifiable();
            var result = _controller.DeleteAgent(new Agent());

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }


        [Fact]
        public void GetAllAgentsList_ReturnsOk()
        {
            _repository.Setup(repo => repo.GetAllAgentsList()).Returns(new List<Agent>()).Verifiable();
            var result = _controller.GetAllAgentsList();

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
