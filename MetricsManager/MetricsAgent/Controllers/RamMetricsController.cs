using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        [HttpGet("available/{availableMb}")]
        public IActionResult GetMetricsAvaliableMemoryMb([FromRoute] int availableMb)
        {
            return Ok();
        }
    }
}
