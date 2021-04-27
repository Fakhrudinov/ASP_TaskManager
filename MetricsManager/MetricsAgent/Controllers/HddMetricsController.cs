using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        [HttpGet("left/{availableMb}")]
        public IActionResult GetMetricsHDDLeftMb([FromRoute] int availableMb)
        {
            return Ok();
        }
    }
}
