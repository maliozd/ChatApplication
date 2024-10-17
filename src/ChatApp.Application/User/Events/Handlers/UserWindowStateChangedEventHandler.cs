using Application.Common.Interfaces.Hubs;
using ChatApp.Application.Common.Interfaces;
using ChatApp.Application.Common.Interfaces.Repository;
using ChatApp.Domain.Entities;
using ChatApp.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ChatApp.Application.User.Events.Handlers
{
    internal class UserOnlineStatusChangedEventHandler(IUserRepository _userRepository, IMessageHubService _messageHubService, ILogger<UserOnlineStatusChangedEventHandler> _logger, IConnectionPool _connectionPool) : INotificationHandler<UserOnlineStatusChangedEvent>
    {
        public async Task Handle(UserOnlineStatusChangedEvent notification, CancellationToken cancellationToken)
        {
            var connectionIds = _connectionPool
                .GetConnectionIds(notification.UserId)
                .ToArray();
            await _messageHubService.ChangeOnlineStatusAsync(connectionIds, notification.UserId.ToString(), notification.IsWindowVisible);
            AppUser user = await _userRepository.GetByIdAsync(notification.UserId, cancellationToken);
            _logger.LogInformation($"User {user.Username}'s now online --> {notification.IsWindowVisible}");

            if (!notification.IsWindowVisible)
            {

                user.LastSeen = DateTime.Now;
                var result = await _userRepository.UpdateAsync(user, cancellationToken);
                if (result > 0)
                {
                    _logger.LogInformation($"User {user.Username}'s LastSeen updated to {user.LastSeen}");
                }
                else
                {
                    _logger.LogWarning($"Failed to update LastSeen for user {user.Username}");
                }
            }
        }

    }
}
