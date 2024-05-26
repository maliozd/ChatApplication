using ChatApp.Application.Common.Interfaces;
using ChatApp.Domain.Events;
using MediatR;

namespace ChatApp.Application.Events.Handlers
{
    internal class UserDisconnectedEventHandler(IConnectionPool _connectionPool) : INotificationHandler<UserDisconnectedEvent>
    {

        public Task Handle(UserDisconnectedEvent notification, CancellationToken cancellationToken)
        {
            _connectionPool.RemoveConnection(notification.UserId, notification.ConnectionId);
            return Task.CompletedTask;
        }
    }
}
