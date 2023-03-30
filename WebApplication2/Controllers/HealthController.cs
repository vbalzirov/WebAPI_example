using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetPing()
        {
            return Ok();
        }

        [Route("GetHeathData")]
        [HttpGet]
        public IEnumerable<HealthData> GetHeathData()
        {
            return new List<HealthData> { new HealthData() };
        }
    }

    public class HealthData
    {
        public string ServiceName { get; set; } = "Service";

        public long UptimeSeconds { get; set; } = 100;
    }
}
