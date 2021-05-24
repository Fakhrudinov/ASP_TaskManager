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

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetFromTimeToTime([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"{DateTime.Now.ToString("HH:mm:ss:fffffff")}: MetricsAgent/api/Hddmetrics/from/{fromTime}/to/{toTime}");

            IList<HddMetric> metrics = repository.GetFromTimeToTime(fromTime.ToUnixTimeSeconds(), toTime.ToUnixTimeSeconds());

            var response = new HddMetricsResponse()
            {
                Metrics = new List<Responses.HddMetricDto>()
            };

            foreach (var metric in metrics)
            {
                //response.Metrics.Add(new Responses.HddMetricDto { Time = metric.Time, Value = metric.Value, Id = metric.Id });
                response.Metrics.Add(mapper.Map<Responses.HddMetricDto>(metric));
            }

            return Ok(response);
        }
    }
}