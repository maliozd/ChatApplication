using ChatApp.Application.Common.Dtos.Message;
using ChatApp.Application.Common.Interfaces.Repository;
using MediatR;

namespace ChatApp.Application.Message.Get
{
    public class GetMessagesQueryHandler(IChatMessageRepository _chatMessageRepository, IChatMessageRepository chatMessageRepository) : IRequestHandler<GetMessagesQuery, List<MessageDto>>
    {
        public async Task<List<MessageDto>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
        {
            var messages = await _chatMessageRepository.GetUserMessagesAsync(request.UserId, cancellationToken);

            return messages.
                Select(msg =>
            new MessageDto(msg.Id,
            msg.MessageText,
            msg.Sender.Username,
            msg.Sender.Id,
            msg.ReadStatus,
            msg.Timestamp)
            ).ToList();
        }
    }
}
