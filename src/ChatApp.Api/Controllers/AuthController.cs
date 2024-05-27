using ChatApp.Api.Responses;
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
        public async Task<ApiResponse<bool>> Register(CreateUserCommand createUserCommand)
        {
            createUserCommand.IPAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            var response = await _mediator.Send(createUserCommand);
            if (response > 0)
                return ApiResponse<bool>.Success(true);
            else
                return ApiResponse<bool>.Error();
        }

        [HttpPost]
        public async Task<ApiResponse<object>> Login(LoginUserCommand loginUserCommand)
        {
            var token = await _mediator.Send(loginUserCommand);
            return ApiResponse<object>.Success(new
            {
                Token = token
            }, "Login Success");
        }
    }
}
