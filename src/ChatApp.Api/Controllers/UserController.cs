using ChatApp.Api.Responses;
using ChatApp.Application.Common.Dtos.User;
using ChatApp.Application.User.Queries.Get;
using ChatApp.Application.User.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Api.Controllers
{
    [Route("api/[controller]/")]
    [Authorize("default")]
    [ApiController]
    public class UserController(IMediator _mediator) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ApiResponse<UserDto>> Get(int id)
        {
            UserDto response = await _mediator.Send(new GetUserByIdQuery(id));
            return ApiResponse<UserDto>.Success(response);
        }
        [HttpGet]
        public async Task<ApiResponse<List<UserDto>>> Get()
        {
            var response = await _mediator.Send(new GetUsersQuery());
            return ApiResponse<List<UserDto>>.Success(response);
        }
    }
}
