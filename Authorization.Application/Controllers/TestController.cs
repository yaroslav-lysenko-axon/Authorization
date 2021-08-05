using System;
using Microsoft.AspNetCore.Mvc;
#pragma warning disable CA1822

namespace Authorization.Application.Controllers
{
    [ApiController]
    [Route("test")]
    public class TestController : ControllerBase
    {
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("pong");
        }

        [HttpGet("exception")]
        public IActionResult Exception()
        {
            throw new Exception("This is an intentionally thrown exception");
        }
    }
}
