using ChatApp.Application.Common.Dtos.Message;
using ChatApp.Application.Common.Interfaces.Repository;
using MediatR;

namespace ChatApp.Application.Message.Queries.Get
{
    public class GetMessagesQueryHandler(IChatMessageRepository _chatMessageRepository, IChatMessageRepository chatMessageRepository) : IRequestHandler<GetMessagesQuery, MessagesDto>
    {
        public async Task<MessagesDto> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
        {
            var messages = await _chatMessageRepository.GetUserMessagesByIdAsync(request.UserId, cancellationToken);
            return messages;
        }
    }
}
