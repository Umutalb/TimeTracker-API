using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeTracker.API.Services;
using TimeTracker.API.Models;
using TimeTracker.API.Interfaces;

namespace TimeTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeTrackerController : ControllerBase
    {
        private readonly ITimeTrackerService _service;

        public TimeTrackerController(ITimeTrackerService service)
        {
            _service = service;
        }

        [HttpPost("start")]
        public IActionResult Start()
        {
            _service.Start();

            return Ok(new { message = "Timer started." });
        }

        [HttpPost("stop")]
        public IActionResult Stop()
        {
            try
            {
                var result = _service.Stop();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest( new { error = ex.Message });
            }
        }

        [HttpGet("status")]
        public IActionResult Status()
        {
            var (isRunning, startedAt) = _service.GetStatus();

            return Ok(new { isRunning, startedAt });
        }

        [HttpGet("total")]
        public IActionResult GetTotal()
        {
            var total = _service.GetTotalMinutes();
            return Ok(new { totalMinutes = total });
        }

        [HttpGet("history")]
        public IActionResult GetHistory()
        {
            var sessions = _service.GetAllSessions();
            return Ok(sessions);
        }
    }
}
