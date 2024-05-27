using ChatApp.Api.Responses;
using ChatApp.Application.Common.Dtos.Message;
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
    public class MessageController(IMediator mediator) : BaseController
    {
        readonly IMediator _mediator = mediator;
        [HttpPost]
        public async Task<ApiResponse<bool>> SendMessage(SendPrivateMessageCommand sendPrivateMessageCommand)
        {
            //dispatcher ??? burada araya bir katman daha?
            //logici dağıtmak için?
            await _mediator.Send(sendPrivateMessageCommand);

            var token = HttpContext.Request.Headers.Authorization;
            Console.WriteLine(token.ToString());

            return ApiResponse<bool>.Success(true);
        }
        [HttpGet]
        public async Task<ApiResponse<UserMessagesDto>> Get()
        {
            var userId = GetUserId();
            if (userId > 0)
            {
                var response = await _mediator.Send(new GetMessagesQuery(userId));
                return ApiResponse<UserMessagesDto>.Success(response);
            }
            return ApiResponse<UserMessagesDto>.Error("User not found !");
        }
    }
}
