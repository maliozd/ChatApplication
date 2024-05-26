using ChatApp.Application.Message;
using ChatApp.Application.Message.Get;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Api.Controllers
{
    /// <summary>
    /// controllerlar ı rest mantığında düşün --> 
    /// 
    /// message -> post put get delete ---> id actions
    /// </summary>
    /// <param name="mediator"></param>
    [Route("api/message/")]
    [Authorize("default")]
    [ApiController]
    public class MessageController(IMediator mediator) : CustomBaseController
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
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = GetUserId();
            if (userId > 0)
            {
                var response = await _mediator.Send(new GetMessagesQuery(userId));
                return Ok(response);
            }
            return Ok();
        }
    }
}
