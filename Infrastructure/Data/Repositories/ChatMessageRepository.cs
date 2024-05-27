using ChatApp.Application.Common.Dtos.Message;
using ChatApp.Application.Common.Dtos.User;
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
            await _dbContext.SaveChangesAsync();
        }
        public async Task<UserMessagesDto> GetUserMessagesByIdAsync(int userId, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .Include(u => u.SentMessages)
                .ThenInclude(m => m.Receiver)
                .Include(u => u.ReceivedMessages)
                .ThenInclude(m => m.Sender)
                .FirstOrDefaultAsync(u => u.Id == userId) ?? throw new CustomException("User not found!");

            var allMessages = user.SentMessages
                             .Union(user.ReceivedMessages)
                             .OrderByDescending(m => m.Timestamp)
                             .Select(m => new ChatMessageDto(
                                 m.Id,
                                 m.MessageText,
                                 new UserDto(
                                     m.Sender.Id,
                                     m.Sender.Username,
                                     m.Sender.ProfilePicturePath
                                 ),
                                 new UserDto(
                                     m.Receiver.Id,
                                     m.Receiver.Username,
                                     m.Receiver.ProfilePicturePath
                                 ),
                                 m.Timestamp
                             )).ToList();

            return new UserMessagesDto(
                UserId: user.Id,
                Messages: allMessages
            );
        }
    }
}
