using ChatApp.Application.Common.Dtos.Message;
using ChatApp.Domain.Entities;

namespace ChatApp.Application.Common.Interfaces.Repository
{
    public interface IChatMessageRepository
    {
        Task AddAsync(ChatMessage chatMessage, CancellationToken cancellationToken);
        Task<UserMessagesDto> GetUserMessagesByIdAsync(int userId, CancellationToken cancellationToken);
    }
}
