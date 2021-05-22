using Microsoft.AspNetCore.Mvc;
using System;
using MetricsManager.DAL;
using MetricsManager.Responses;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly ILogger<RamMetricsController> _logger;
        private IRamMetricsRepository repository;
        private readonly IMapper mapper;

        public RamMetricsController(ILogger<RamMetricsController> logger, IRamMetricsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в RamMetricsController");
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgentIdTimeToTime(
            [FromRoute] int agentId,
            [FromRoute] DateTimeOffset fromTime,
            [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"{DateTime.Now.ToString("HH:mm:ss:fffffff")}: MetricsManager/api/rammetrics/agent/{agentId}/from/{fromTime}/to/{toTime}");

            IList<RamMetric> metrics = repository.GetMetricsFromAgentIdTimeToTime(agentId, fromTime.ToUnixTimeSeconds(), toTime.ToUnixTimeSeconds());

            var response = new RamMetricsResponse()
            {
                Metrics = new List<Responses.RamMetricDto>()
            };

            if (metrics != null)
            {
                foreach (var metric in metrics)
                {
                    response.Metrics.Add(mapper.Map<Responses.RamMetricDto>(metric));
                }
            }

            return Ok(response);
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllClusterTimeToTime(
            [FromRoute] DateTimeOffset fromTime,
            [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"{DateTime.Now.ToString("HH:mm:ss:fffffff")}: MetricsManager/api/rammetrics/cluster/from/{fromTime}/to/{toTime}");

            IList<RamMetric> metrics = repository.GetMetricsFromAllClusterTimeToTime(fromTime.ToUnixTimeSeconds(), toTime.ToUnixTimeSeconds());

            var response = new RamMetricsResponse()
            {
                Metrics = new List<Responses.RamMetricDto>()
            };

            if (metrics != null)
            {
                foreach (var metric in metrics)
                {
                    response.Metrics.Add(mapper.Map<Responses.RamMetricDto>(metric));
                }
            }

            return Ok(response);
        }
    }
}


