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
    public class CpuMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;
        private ICpuMetricsRepository repository;

        public CpuMetricsController(ILogger<CpuMetricsController> logger, ICpuMetricsRepository repository)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsController");
            this.repository = repository;
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgentIdTimeToTime(
            [FromRoute] int agentId, 
            [FromRoute] DateTimeOffset fromTime, 
            [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"{DateTime.Now.ToString("HH:mm:ss:fffffff")}: MetricsManager/api/cpumetrics/agent/{agentId}/from/{fromTime}/to/{toTime}");

            IList<CpuMetric> metrics = repository.GetMetricsFromAgentIdTimeToTime(agentId, fromTime.ToUnixTimeSeconds(), toTime.ToUnixTimeSeconds());

            var response = new CpuMetricsResponse()
            {
                Metrics = new List<Responses.CpuMetricDto>()
            };

            if (metrics != null)
            {
                foreach (var metric in metrics)
                {
                    response.Metrics.Add(new Responses.CpuMetricDto { Time = metric.Time, Value = metric.Value, Id = metric.Id });
                }
            }

            return Ok(response);
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllClusterTimeToTime(
            [FromRoute] DateTimeOffset fromTime,
            [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"{DateTime.Now.ToString("HH:mm:ss:fffffff")}: MetricsManager/api/cpumetrics/cluster/from/{fromTime}/to/{toTime}");

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

