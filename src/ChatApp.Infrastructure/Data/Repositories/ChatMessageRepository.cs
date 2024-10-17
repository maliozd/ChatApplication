using ChatApp.Application.Common.Dtos.Message;
using ChatApp.Application.Common.Exceptions;
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
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        public async Task<MessagesDto> GetUserMessagesByIdAsync(int userId, CancellationToken cancellationToken)
        {
            //var user = await _dbContext.Users
            //    .Include(u => u.SentMessages)
            //    .ThenInclude(m => m.Receiver)
            //    .Include(u => u.ReceivedMessages)
            //    .ThenInclude(m => m.Sender)
            //    .FirstOrDefaultAsync(u => u.Id == userId) ?? throw new CustomException("User not found!");

            //var allMessages = user.SentMessages
            //                 .Union(user.ReceivedMessages)
            //                 .OrderByDescending(m => m.Timestamp)
            //                 .Select(m => new ChatMessageDto(
            //                     m.Id,
            //                     m.MessageText,
            //                     m.Sender.Id,
            //                     m.Receiver.Id,
            //                     m.Timestamp
            //                 )).ToList();

            //return new UserMessagesDto(
            //    UserId: user.Id,
            //    Messages: allMessages
            //);

            var user = await _dbContext.Users
    .Include(u => u.SentMessages)
    .ThenInclude(m => m.Receiver)
    .Include(u => u.ReceivedMessages)
    .ThenInclude(m => m.Sender)
    .FirstOrDefaultAsync(u => u.Id == userId) ?? throw new CustomException("User not found!");


            var allMessages = user.SentMessages
                    .Union(user.ReceivedMessages)
                    .OrderBy(m => m.Timestamp)
                    .ToList();

            var groupedMessages = allMessages
       .GroupBy(m => m.Sender.Id == userId ? m.Receiver : m.Sender)
       .Select(g => new UserMessagesDto(
           UserId: g.Key.Id,
           Messages: g.Select(m => new ChatMessageDto(
               m.Id,
               m.MessageText,
               m.Sender.Id,
               m.Receiver.Id,
               m.Timestamp
           )).ToList()
       )).ToList();


            return new MessagesDto(
     Messages: groupedMessages
 );
        }
    }

}
