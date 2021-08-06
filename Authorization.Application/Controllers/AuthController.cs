using System.Threading.Tasks;
using Authorization.Application.Models.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.Application.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet("token")]
        public async Task<IActionResult> GetUserByEmailAndPassword([FromQuery] EnterUserCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
