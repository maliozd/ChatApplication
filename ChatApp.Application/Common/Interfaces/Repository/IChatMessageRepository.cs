using ChatApp.Domain.Entities;

namespace ChatApp.Application.Common.Interfaces.Repository
{
    public interface IChatMessageRepository
    {
        Task AddAsync(ChatMessage chatMessage, CancellationToken cancellationToken);
        Task<IEnumerable<ChatMessage>> GetUserMessagesAsync(int userId, CancellationToken cancellationToken);
    }
}
