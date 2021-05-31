using Microsoft.AspNetCore.Mvc;
using System;
using MetricsAgent.DAL;
using MetricsAgent.Responses;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace MetricsAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NetWorkMetricsController : ControllerBase
    {
        private readonly ILogger<NetWorkMetricsController> _logger;
        private INetWorkMetricsRepository repository;
        private readonly IMapper mapper;

        public NetWorkMetricsController(ILogger<NetWorkMetricsController> logger, INetWorkMetricsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в NetWorkMetricsController");
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetFromTimeToTime([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"{DateTime.Now.ToString("HH:mm:ss:fffffff")}: MetricsAgent/api/NetWorkmetrics/from/{fromTime}/to/{toTime}");

            IList<NetWorkMetric> metrics = repository.GetFromTimeToTime(fromTime.ToUnixTimeSeconds(), toTime.ToUnixTimeSeconds());

            var response = new NetWorkMetricsResponse()
            {
                Metrics = new List<Responses.NetWorkMetricDto>()
            };

            foreach (var metric in metrics)
            {
                //response.Metrics.Add(new Responses.NetWorkMetricDto { Time = metric.Time, Value = metric.Value, Id = metric.Id });
                response.Metrics.Add(mapper.Map<Responses.NetWorkMetricDto>(metric));
            }

            return Ok(response);
        }
    }
}
