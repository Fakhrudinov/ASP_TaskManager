using Microsoft.AspNetCore.Mvc;
using System;
using MetricsAgent.DAL;
using MetricsAgent.Responses;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers
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

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetFromTimeToTime([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"{DateTime.Now.ToString("HH:mm:ss:fffffff")}: MetricsAgent/api/cpumetrics/from/{fromTime}/to/{toTime}");

            IList<CpuMetric> metrics = repository.GetFromTimeToTime(fromTime.ToUnixTimeSeconds(), toTime.ToUnixTimeSeconds());

            var response = new CpuMetricsResponse()
            {
                Metrics = new List<Responses.CpuMetricDto>()
            };

            if(metrics != null)
            {
                foreach (var metric in metrics)
                {
                    response.Metrics.Add(new Responses.CpuMetricDto { Time = metric.Time, Value = metric.Value, Id = metric.Id });
                }
            }

            return Ok(response);
        }

        //[HttpPost("create")]
        //public IActionResult Create([FromBody] CpuMetricCreateRequest request)
        //{
        //    _logger.LogInformation($"{DateTime.Now.ToString("HH:mm:ss:fffffff")}: MetricsAgent/api/cpumetrics/create, Time={request.Time}, Value={request.Value}");

        //    repository.Create(new CpuMetric
        //    {
        //        Time = request.Time.ToUniversalTime(),
        //        Value = request.Value
        //    });

        //    return Ok();
        //}

        //[HttpGet("all")]
        //public IActionResult GetAll()
        //{
        //    _logger.LogInformation($"{DateTime.Now.ToString("HH:mm:ss:fffffff")}: MetricsAgent/api/cpumetrics/all");

        //    IList<CpuMetric> metrics = repository.GetAll();

        //    var response = new AllCpuMetricsResponse()
        //    {
        //        Metrics = new List<Responses.CpuMetricDto>()
        //    };

        //    foreach (var metric in metrics)
        //    {
        //        response.Metrics.Add(new Responses.CpuMetricDto { Time = metric.Time, Value = metric.Value, Id = metric.Id });
        //    }

        //    return Ok(response);
        //}


        //[HttpGet("from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        //public IActionResult GetMetricsFromTimeToTimeByPercentile(
        //    [FromRoute] TimeSpan fromTime, 
        //    [FromRoute] TimeSpan toTime, 
        //    [FromRoute] Percentile percentile)
        //{
        //    return Ok();
        //}
    }
}


