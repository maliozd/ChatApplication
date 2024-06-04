using Application.Common.Interfaces.Hubs;
using ChatApp.Application.Common.Interfaces.Repository;
using ChatApp.Domain.Entities;
using ChatApp.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ChatApp.Application.User.Events.Handlers
{
    internal class UserWindowStateChangedEventHandler(IUserRepository _userRepository, IMessageHubService _messageHubService, ILogger<UserWindowStateChangedEventHandler> _logger) : INotificationHandler<UserWindowStateChangedEvent>
    {
        public async Task Handle(UserWindowStateChangedEvent notification, CancellationToken cancellationToken)
        {
            await _messageHubService.ChangeOnlineStatusAsync(notification.UserId, notification.IsWindowVisible);
            if (!notification.IsWindowVisible)
            {
                AppUser user = await _userRepository.GetByIdAsync(notification.UserId, cancellationToken);

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
