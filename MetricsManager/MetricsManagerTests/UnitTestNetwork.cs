﻿using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using Microsoft.Extensions.Logging;
using Moq;
using AutoMapper;
using MetricsManager.DataAccessLayer.Repository;
using System.Collections.Generic;
using MetricsManager;

namespace MetricsManagerTests
{
    public class UnitTestNetwork
    {
        private NetworkMetricsController controller;
        private Mock<INetWorkMetricsRepository> mock;
        private Mock<ILogger<NetworkMetricsController>> logger;
        private readonly IMapper mapper;

        public UnitTestNetwork()
        {
            mock = new Mock<INetWorkMetricsRepository>();
            logger = new Mock<ILogger<NetworkMetricsController>>();
            controller = new NetworkMetricsController(logger.Object, mock.Object, mapper);
        }

        [Fact]
        public void GetMetricsFromAgentIdTimeToTime_ReturnsOk()
        {
            //Arrange
            var agentId = 1;
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(10);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            mock.Setup(a => a.GetMetricsFromAgentIdTimeToTime(agentId, 10, 100)).Returns(new List<NetWorkMetric>()).Verifiable();

            //Act
            var result = controller.GetMetricsFromAgentIdTimeToTime(agentId, fromTime, toTime);
            //Assert
            mock.Verify(repository => repository.GetMetricsFromAgentIdTimeToTime(1, 10, 100), Times.AtMostOnce());
            logger.Verify();
        }

        [Fact]
        public void GetMetricsFromAllClusterTimeToTime_ReturnsOk()
        {
            //Arrange
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(10);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            mock.Setup(a => a.GetMetricsFromAllClusterTimeToTime(10, 100)).Returns(new List<NetWorkMetric>()).Verifiable();

            //Act
            var result = controller.GetMetricsFromAllClusterTimeToTime(fromTime, toTime);
            //Assert
            mock.Verify(repository => repository.GetMetricsFromAgentIdTimeToTime(1, 10, 100), Times.AtMostOnce());
            logger.Verify();
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
