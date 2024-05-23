using ChatApp.Domain.Entities;

namespace ChatApp.Application.Common.Interfaces.Repository
{
    public interface IChatMessageRepository
    {
        Task AddAsync(ChatMessage chatMessage, CancellationToken cancellationToken);
    }
}
