using Bank.Api.Controllers;
using Bank.Application.Core.Identity.commands.Login;
using Bank.Application.Core.Identity.commands.Register;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bankproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : BaseController
    {
        public AuthenticationController(IMediator _mediator):base(_mediator) {}
        [HttpPost("register")]
        public async Task<IActionResult> Register(registercommand command)
        {
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(logincommand command)
        {
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }

    }
}
