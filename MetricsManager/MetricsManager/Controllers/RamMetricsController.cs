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

        /// <summary>
        /// Запрос метрик RAM (размер доступной памяти системы агента) на заданном диапазоне времени от указанного агента. 
        /// Время указывается как DateTimeOffset. Параметры указываются в пути запроса (FromRoute)
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///     api/rammetrics/agent/1/from/2021-05-19T03:15:00/to/2021-05-20T16:30:00
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
            _logger.LogInformation($"{DateTime.Now.ToString("HH:mm:ss:fffffff")}: MetricsManager/api/rammetrics/agent/{agentId}/from/{fromTime}/to/{toTime}");

            IList<RamMetric> metrics = repository.GetMetricsFromAgentIdTimeToTime(agentId, fromTime.ToUnixTimeSeconds(), toTime.ToUnixTimeSeconds());

            var response = new RamMetricsResponse()
            {
                Metrics = new List<Responses.RamMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(mapper.Map<Responses.RamMetricDto>(metric));
            }

            return Ok(response);
        }

        /// <summary>
        /// Запрос метрик RAM (размер доступной памяти системы агента) на заданном диапазоне времени от всех агентов. 
        /// Время указывается как DateTimeOffset. Параметры указываются в пути запроса (FromRoute)
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///     api/rammetrics/cluster/from/2021-05-19T03:15:00/to/2021-05-20T16:30:00
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
            _logger.LogInformation($"{DateTime.Now.ToString("HH:mm:ss:fffffff")}: MetricsManager/api/rammetrics/cluster/from/{fromTime}/to/{toTime}");

            IList<RamMetric> metrics = repository.GetMetricsFromAllClusterTimeToTime(fromTime.ToUnixTimeSeconds(), toTime.ToUnixTimeSeconds());

            var response = new RamMetricsResponse()
            {
                Metrics = new List<Responses.RamMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(mapper.Map<Responses.RamMetricDto>(metric));
            }

            return Ok(response);
        }
    }
}


