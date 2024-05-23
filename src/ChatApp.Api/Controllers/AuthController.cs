using ChatApp.Application.User.Commands.CreateUser;
using ChatApp.Application.User.Commands.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController(IMediator _mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Register(CreateUserCommand createUserCommand)
        {
            createUserCommand.IPAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            var response = await _mediator.Send(createUserCommand);
            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginUserCommand loginUserCommand)
        {
            var token = await _mediator.Send(loginUserCommand);
            return Ok(token);
        }
    }
}
