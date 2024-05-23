using MediatR;

namespace ChatApp.Application.Message
{
    public class SendPrivateMessageCommand : IRequest
    {
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public string MessageText { get; set; }
    }
}
