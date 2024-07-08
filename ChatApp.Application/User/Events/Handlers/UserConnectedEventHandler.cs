using Application.Common.Interfaces.Hubs;
using ChatApp.Application.Common.Interfaces;
using ChatApp.Domain.Events;
using MediatR;

namespace ChatApp.Application.Events.Handlers
{
    internal class UserConnectedEventHandler(IConnectionPool _connectionPool, IMessageHubService _messageHubService) : INotificationHandler<UserConnectedEvent>
    {
        public async Task Handle(UserConnectedEvent notification, CancellationToken cancellationToken)
        {
            _connectionPool.AddConnection(notification.UserId, notification.ConnectionId);
            var userConnectionIds = _connectionPool.GetConnectionIds(notification.UserId).ToArray();

            await _messageHubService.ChangeOnlineStatusAsync(userConnectionIds, notification.UserId.ToString(), true);
            await Task.CompletedTask;
        }
    }

}
