using Microsoft.AspNetCore.Mvc;
using System;
using MetricsManager.Responses;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using AutoMapper;
using MetricsManager.DataAccessLayer.Repository;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;
        private ICpuMetricsRepository repository;
        private readonly IMapper mapper;

        public CpuMetricsController(ILogger<CpuMetricsController> logger, ICpuMetricsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsController");
            this.repository = repository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Запрос метрик CPU (занятость в % всех процессоров системы агента) на заданном диапазоне времени от указанного агента. 
        /// Время указывается как DateTimeOffset. Параметры указываются в пути запроса (FromRoute)
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///     api/cpumetrics/agent/1/from/2021-05-19T03:15:00/to/2021-05-20T16:30:00
        /// </remarks>
        /// <param name="agentId">Идентификатор агента</param>
        /// <param name="fromTime">начальная метрика времени</param>
        /// <param name="toTime">конечная метрика времени</param>
        /// <returns>Список метрик, которые были сохранены в заданном диапазоне времени</returns>
        /// <response code="200">Если все хорошо</response>
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

            foreach (var metric in metrics)
            {
                response.Metrics.Add(mapper.Map<Responses.CpuMetricDto>(metric));
            }

            return Ok(response);
        }

        /// <summary>
        /// Запрос метрик CPU (занятость в % всех процессоров системы агента) на заданном диапазоне времени от всех агентов. 
        /// Время указывается как DateTimeOffset. Параметры указываются в пути запроса (FromRoute)
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///     api/cpumetrics/cluster/from/2021-05-19T03:15:00/to/2021-05-20T16:30:00
        /// </remarks>
        /// <param name="fromTime">начальная метрика времени</param>
        /// <param name="toTime">конечная метрика времени</param>
        /// <returns>Список метрик, которые были сохранены в заданном диапазоне времени</returns>
        /// <response code="200">Если все хорошо</response>
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllClusterTimeToTime(
            [FromRoute] DateTimeOffset fromTime,
            [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"{DateTime.Now.ToString("HH:mm:ss:fffffff")}: MetricsManager/api/cpumetrics/cluster/from/{fromTime}/to/{toTime}");

            IList<CpuMetric> metrics = repository.GetMetricsFromAllClusterTimeToTime(fromTime.ToUnixTimeSeconds(), toTime.ToUnixTimeSeconds());

            var response = new CpuMetricsResponse()
            {
                Metrics = new List<Responses.CpuMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(mapper.Map<Responses.CpuMetricDto>(metric));
            }

            return Ok(response);
        }


    }
}

