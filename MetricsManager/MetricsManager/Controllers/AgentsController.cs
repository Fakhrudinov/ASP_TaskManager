using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly List<AgentInfo> _registeredAgents;
        public AgentsController(List<AgentInfo> registeredAgents)
        {
            _registeredAgents = registeredAgents;
        }

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            _registeredAgents.Add(agentInfo);
            return Ok();
        }

        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            return Ok();
        }

        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            return Ok();
        }

        [HttpGet("getAgentsList")]
        public IActionResult ListAgents()
        {
            return Ok(_registeredAgents.ToArray());
        }
    }
}