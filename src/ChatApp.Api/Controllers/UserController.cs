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
        public async Task<IActionResult> Get(int id)
        {
            var response = await _mediator.Send(new GetUserByIdQuery(id));
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _mediator.Send(new GetUsersQuery());

            return Ok(response);
        }
    }
}
