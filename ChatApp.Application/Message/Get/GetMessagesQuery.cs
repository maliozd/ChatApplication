using ChatApp.Application.Common.Dtos.Message;
using MediatR;

namespace ChatApp.Application.Message.Get
{
    public record GetMessagesQuery(int UserId) : IRequest<UserMessagesDto>
    {
    }
}
