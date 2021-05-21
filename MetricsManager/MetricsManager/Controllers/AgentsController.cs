using Microsoft.AspNetCore.Mvc;
using System;
using MetricsManager.DAL;
using MetricsManager.Responses;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly List<AgentInfo> _registeredAgents;
        private readonly ILogger<AgentsController> _logger;

        public AgentsController(List<AgentInfo> registeredAgents, ILogger<AgentsController> logger)
        {
            _registeredAgents = registeredAgents;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в AgentsController");
        }


        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            _logger.LogTrace($"{DateTime.Now.ToString("HH: mm:ss: fffffff")}: AgentsController/api/register: AgentID={agentInfo.AgentId}, AgentAdress={agentInfo.AgentAddress}");
            _registeredAgents.Add(agentInfo);
            return Ok();
        }

        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            _logger.LogTrace($"{DateTime.Now.ToString("HH: mm:ss: fffffff")}: AgentsController/api/enable/{agentId}");

            return Ok();
        }

        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            _logger.LogTrace($"{DateTime.Now.ToString("HH: mm:ss: fffffff")}: AgentsController/api/disable/{agentId}");

            return Ok();
        }

        [HttpGet("getAgentsList")]
        public IActionResult ListAgents()
        {
            _logger.LogTrace($"{DateTime.Now.ToString("HH: mm:ss: fffffff")}: AgentsController/api/getAgentsList");

            return Ok(_registeredAgents.ToArray());
        }
    }
}