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

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetFromTimeToTime([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"{DateTime.Now.ToString("HH:mm:ss:fffffff")}: MetricsAgent/api/Rammetrics/from/{fromTime}/to/{toTime}");

            IList<RamMetric> metrics = repository.GetFromTimeToTime(fromTime.ToUnixTimeSeconds(), toTime.ToUnixTimeSeconds());

            var response = new RamMetricsResponse()
            {
                Metrics = new List<Responses.RamMetricDto>()
            };

            if (metrics != null)
            {
                foreach (var metric in metrics)
                {
                    //response.Metrics.Add(new Responses.RamMetricDto { Time = metric.Time, Value = metric.Value, Id = metric.Id });
                    response.Metrics.Add(mapper.Map<Responses.RamMetricDto>(metric));
                }
            }

            return Ok(response);
        }
    }
}