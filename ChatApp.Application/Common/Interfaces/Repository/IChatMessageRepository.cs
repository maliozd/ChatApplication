using ChatApp.Application.Common.Dtos.Message;
using ChatApp.Domain.Entities;

namespace ChatApp.Application.Common.Interfaces.Repository
{
    public interface IChatMessageRepository
    {
        Task AddAsync(ChatMessage chatMessage, CancellationToken cancellationToken);
        Task<MessagesDto> GetUserMessagesByIdAsync(int userId, CancellationToken cancellationToken);
    }
}
