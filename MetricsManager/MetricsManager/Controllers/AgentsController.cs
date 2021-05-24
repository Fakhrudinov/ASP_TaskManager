using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;
using MetricsManager.DataAccessLayer.Repository;
using MetricsManager.DTO;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly ILogger<AgentsController> _logger;
        private readonly IAgentsRepository _repository;

        public AgentsController(ILogger<AgentsController> logger, IAgentsRepository repository)
        {
            _repository = repository;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в AgentsController");
        }

        /// <summary>
        /// Добавляем нового агента в список зарегистрированных в системе агентов. Адрес подключения должен быть уникален. Id агента - не используется
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///    /api/Agents/register
        /// 
        /// <example>
        /// JSON in body:
        ///<code>
        ///     {
        ///         "AgentAddress": "http://localhost:5070"
        ///     }
        ///</code>
        /// </example>
        /// </remarks>
        /// <param name="agent"></param>

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] Agent agent)
        {
            _logger.LogTrace($"{DateTime.Now.ToString("HH: mm:ss: fffffff")}: AgentsController/register: AgentAdress={agent.AgentAddress}");
            _repository.RegisterAgent(agent);

            return Ok();
        }

        /// <summary>
        /// Удаляем агента из зарегистрированных в системе агентов. Удаление происходит по адресу подключения ранее добавленного агента.  Id агента - не используется
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///    /api/Agents/delete
        /// 
        /// <example>
        /// JSON in body:
        ///<code>
        ///     {
        ///         "AgentAddress": "http://localhost:5070"
        ///     }
        ///</code>
        /// </example>
        /// </remarks>
        /// <param name="agent"></param>
        [HttpPost("delete")]
        public IActionResult DeleteAgent([FromBody] Agent agent)
        {
            _logger.LogTrace($"{DateTime.Now.ToString("HH: mm:ss: fffffff")}: AgentsController/delete: AgentAdress={agent.AgentAddress}");
            _repository.RemoveAgent(agent);
            return Ok();
        }

        /// <summary>
        /// Получить всех зарегистрированных в системе агентов.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///    /api/Agents/getAgentsList
        /// </remarks>
        [HttpGet("getAgentsList")]
        public IActionResult GetAllAgentsList()
        {
            _logger.LogTrace($"{DateTime.Now.ToString("HH: mm:ss: fffffff")}: AgentsController/GetAllAgentsList");
            var agentList = _repository.GetAllAgentsList();
            return Ok(agentList);   
        }
    }
}