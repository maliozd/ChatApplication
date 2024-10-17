using ChatApp.Application.Common.Dtos.Message;
using MediatR;

namespace ChatApp.Application.Message.Queries.Get
{
    public record GetMessagesQuery(int UserId) : IRequest<MessagesDto>
    {
    }
}
