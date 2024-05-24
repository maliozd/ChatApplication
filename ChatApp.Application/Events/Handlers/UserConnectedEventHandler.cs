using ChatApp.Application.Common.Interfaces;
using ChatApp.Domain.Events;
using MediatR;

namespace ChatApp.Application.Events.Handlers
{
    internal class UserConnectedEventHandler(IConnectionCache _connectionPool) : INotificationHandler<UserConnectedEvent>
    {

        public async Task Handle(UserConnectedEvent notification, CancellationToken cancellationToken)
        {
            // User connected logic
            // Örneğin: Kullanıcı çevrimiçi durumuna geçirilebilir veya log tutulabilir
            _connectionPool.AddConnection(notification.UserId, notification.ConnectionId);
            await Task.CompletedTask;
        }
    }

}
