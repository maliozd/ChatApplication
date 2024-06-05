using Application.Common.Interfaces.Hubs;
using ChatApp.Application.Common.Interfaces;
using ChatApp.Domain.Events;
using MediatR;

namespace ChatApp.Application.Events.Handlers
{
    internal class UserDisconnectedEventHandler(IMessageHubService _messageHubService, IConnectionPool _connectionPool) : INotificationHandler<UserDisconnectedEvent>
    {
        public async Task Handle(UserDisconnectedEvent notification, CancellationToken cancellationToken)
        {
            var connectionIds = _connectionPool.
                GetConnectionIds(notification.UserId)
                .ToArray();
            _connectionPool.RemoveConnection(notification.UserId, notification.ConnectionId);
            await _messageHubService.ChangeOnlineStatusAsync(connectionIds, notification.UserId.ToString(), false);
        }
    }
}
