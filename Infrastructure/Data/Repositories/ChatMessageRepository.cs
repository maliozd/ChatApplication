using ChatApp.Application.Common.Interfaces.Repository;
using ChatApp.Domain.Entities;

namespace Infrastructure.Data.Repositories
{
    public class ChatMessageRepository(AppDbContext _dbContext) : IChatMessageRepository
    {
        public async Task AddAsync(ChatMessage chatMessage, CancellationToken cancellationToken)
        {
            await _dbContext.AddAsync(chatMessage, cancellationToken);
            await _dbContext.SaveChangesAsync();
        }
    }
}
