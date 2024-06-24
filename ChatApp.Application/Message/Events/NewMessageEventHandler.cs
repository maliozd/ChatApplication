using Application.Common.Interfaces.Hubs;
using ChatApp.Application.Common.Interfaces.Repository;
using ChatApp.Domain.Entities;
using ChatApp.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ChatApp.Application.Message.Events
{
    public class NewMessageEventHandler(IMessageHubService messageHubService, IChatMessageRepository _chatMessageRepository, ILogger<NewMessageEventHandler> _logger) : INotificationHandler<NewMessageEvent>
    {
        public async Task Handle(NewMessageEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                await messageHubService.SendMessageAsync(
                       new(notification.MessageText,
                       notification.FromUserId,
                       notification.ToUserId,
                       notification.Timestamp));

                var message = new ChatMessage
                {
                    FromUserId = notification.FromUserId,
                    ToUserId = notification.ToUserId,
                    MessageText = notification.MessageText,
                    Timestamp = DateTime.UtcNow,
                    ReadStatus = false,
                    CreatedDate = DateTime.UtcNow
                };

                await _chatMessageRepository.AddAsync(message, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message, ex.InnerException);
                throw;
            }
        }
    }
}
