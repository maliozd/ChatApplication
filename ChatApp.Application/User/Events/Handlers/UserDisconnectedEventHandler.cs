using Application.Common.Interfaces.Hubs;
using ChatApp.Domain.Events;
using MediatR;

namespace ChatApp.Application.Events.Handlers
{
    internal class UserDisconnectedEventHandler(IMessageHubService _messageHubService) : INotificationHandler<UserDisconnectedEvent>
    {
        public async Task Handle(UserDisconnectedEvent notification, CancellationToken cancellationToken)
        {
            await _messageHubService.ChangeOnlineStatusAsync(notification.UserId, false);
        }
    }
}
