using ChatApp.Application.Message;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Api.Controllers
{
    /// <summary>
    /// controllerlar ı rest mantığında düşün --> 
    /// 
    /// message -> post put get delete ---> id actions
    /// </summary>
    /// <param name="mediator"></param>
    [Route("api/message/[action]")]
    [ApiController]
    public class MessageController(IMediator mediator) : ControllerBase
    {
        readonly IMediator _mediator = mediator;
        [HttpPost]
        public async Task<IActionResult> SendMessage(SendPrivateMessageCommand sendPrivateMessageCommand)
        {
            //dispatcher ??? burada araya bir katman daha?
            //logici dağıtmak için??
            await _mediator.Send(sendPrivateMessageCommand);

            var token = HttpContext.Request.Headers.Authorization;
            Console.WriteLine(token.ToString());

            return Ok("");
        }
    }
}
