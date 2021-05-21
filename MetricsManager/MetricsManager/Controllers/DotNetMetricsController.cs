using Microsoft.AspNetCore.Mvc;
using System;
using MetricsManager.DAL;
using MetricsManager.Responses;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private readonly ILogger<DotNetMetricsController> _logger;
        private IDotNetMetricsRepository repository;

        public DotNetMetricsController(ILogger<DotNetMetricsController> logger, IDotNetMetricsRepository repository)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в DotNetMetricsController");
            this.repository = repository;
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgentIdTimeToTime(
            [FromRoute] int agentId,
            [FromRoute] DateTimeOffset fromTime,
            [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"{DateTime.Now.ToString("HH:mm:ss:fffffff")}: MetricsManager/api/dotnetmetrics/agent/{agentId}/from/{fromTime}/to/{toTime}");

            return Ok();
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllClusterTimeToTime(
            [FromRoute] DateTimeOffset fromTime,
            [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"{DateTime.Now.ToString("HH:mm:ss:fffffff")}: MetricsManager/api/dotnetmetrics/cluster/from/{fromTime}/to/{toTime}");

            return Ok();
        }

        //[HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        //public IActionResult GetMetricsByPercentileFromAgent(
        //    [FromRoute] int agentId,
        //    [FromRoute] TimeSpan fromTime,
        //    [FromRoute] TimeSpan toTime,
        //    [FromRoute] Percentile percentile)
        //{
        //    return Ok();
        //}


        //[HttpGet("cluster/from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        //public IActionResult GetMetricsByPercentileFromAllCluster(
        //    [FromRoute] TimeSpan fromTime,
        //    [FromRoute] TimeSpan toTime,
        //    [FromRoute] Percentile percentile)
        //{
        //    return Ok();
        //}
    }
}

