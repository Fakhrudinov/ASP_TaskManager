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
    public class DotNetMetricsController : ControllerBase
    {
        private readonly ILogger<DotNetMetricsController> _logger;
        private IDotNetMetricsRepository repository;
        private readonly IMapper mapper;

        public DotNetMetricsController(ILogger<DotNetMetricsController> logger, IDotNetMetricsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в DotNetMetricsController");
            this.repository = repository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Запрос метрик DotNet (сумма байт в кучах Gen 1, Gen 2 и Large Object Heap системы агента) на заданном диапазоне времени от указанного агента. 
        /// Время указывается как DateTimeOffset. Параметры указываются в пути запроса (FromRoute)
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///     api/dotnetmetrics/agent/1/from/2021-05-19T03:15:00/to/2021-05-20T16:30:00
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
            _logger.LogInformation($"{DateTime.Now.ToString("HH:mm:ss:fffffff")}: MetricsManager/api/Dotnetmetrics/agent/{agentId}/from/{fromTime}/to/{toTime}");

            IList<DotNetMetric> metrics = repository.GetMetricsFromAgentIdTimeToTime(agentId, fromTime.ToUnixTimeSeconds(), toTime.ToUnixTimeSeconds());

            var response = new DotNetMetricsResponse()
            {
                Metrics = new List<Responses.DotNetMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(mapper.Map<Responses.DotNetMetricDto>(metric));
            }

            return Ok(response);
        }

        /// <summary>
        /// Запрос метрик DotNet (сумма байт в кучах Gen 1, Gen 2 и Large Object Heap системы агента) на заданном диапазоне времени от всех агентов. 
        /// Время указывается как DateTimeOffset. Параметры указываются в пути запроса (FromRoute)
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///     api/dotnetmetrics/cluster/from/2021-05-19T03:15:00/to/2021-05-20T16:30:00
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
            _logger.LogInformation($"{DateTime.Now.ToString("HH:mm:ss:fffffff")}: MetricsManager/api/Dotnetmetrics/cluster/from/{fromTime}/to/{toTime}");

            IList<DotNetMetric> metrics = repository.GetMetricsFromAllClusterTimeToTime(fromTime.ToUnixTimeSeconds(), toTime.ToUnixTimeSeconds());

            var response = new DotNetMetricsResponse()
            {
                Metrics = new List<Responses.DotNetMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(mapper.Map<Responses.DotNetMetricDto>(metric));
            }

            return Ok(response);
        }
    }
}

