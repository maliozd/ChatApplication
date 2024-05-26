using ChatApp.Application.Common.Interfaces.Repository;
using ChatApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class ChatMessageRepository(AppDbContext _dbContext) : IChatMessageRepository
    {
        public async Task AddAsync(ChatMessage chatMessage, CancellationToken cancellationToken)
        {
            await _dbContext.AddAsync(chatMessage, cancellationToken);
            await _dbContext.SaveChangesAsync();
        }

        public Task<IEnumerable<ChatMessage>> GetUserMessagesAsync(int userId, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
             {

                 var messages = _dbContext.Messages.
                 Include(msg => msg.Sender).
                 Include(msg => msg.Receiver).
                 Where(x => x.ToUserId == userId || x.FromUserId == userId)
                 .OrderBy(x => x.Timestamp)
                 .AsEnumerable();
                 return messages;
             });
        }
    }
}
