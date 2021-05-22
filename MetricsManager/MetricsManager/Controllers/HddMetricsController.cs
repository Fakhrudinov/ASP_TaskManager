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
    public class HddMetricsController : ControllerBase
    {
        private readonly ILogger<HddMetricsController> _logger;
        private IHddMetricsRepository repository;
        private readonly IMapper mapper;

        public HddMetricsController(ILogger<HddMetricsController> logger, IHddMetricsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в HddMetricsController");
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgentIdTimeToTime(
           [FromRoute] int agentId,
           [FromRoute] DateTimeOffset fromTime,
           [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"{DateTime.Now.ToString("HH:mm:ss:fffffff")}: MetricsManager/api/hddmetrics/agent/{agentId}/from/{fromTime}/to/{toTime}");

            IList<HddMetric> metrics = repository.GetMetricsFromAgentIdTimeToTime(agentId, fromTime.ToUnixTimeSeconds(), toTime.ToUnixTimeSeconds());

            var response = new HddMetricsResponse()
            {
                Metrics = new List<Responses.HddMetricDto>()
            };

            if (metrics != null)
            {
                foreach (var metric in metrics)
                {
                    response.Metrics.Add(mapper.Map<Responses.HddMetricDto>(metric));
                }
            }

            return Ok(response);
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllClusterTimeToTime(
            [FromRoute] DateTimeOffset fromTime,
            [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"{DateTime.Now.ToString("HH:mm:ss:fffffff")}: MetricsManager/api/hddmetrics/cluster/from/{fromTime}/to/{toTime}");

            IList<HddMetric> metrics = repository.GetMetricsFromAllClusterTimeToTime(fromTime.ToUnixTimeSeconds(), toTime.ToUnixTimeSeconds());

            var response = new HddMetricsResponse()
            {
                Metrics = new List<Responses.HddMetricDto>()
            };

            if (metrics != null)
            {
                foreach (var metric in metrics)
                {
                    response.Metrics.Add(mapper.Map<Responses.HddMetricDto>(metric));
                }
            }

            return Ok(response);
        }
    }
}

