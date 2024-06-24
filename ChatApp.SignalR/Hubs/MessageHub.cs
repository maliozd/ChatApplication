using ChatApp.Application.Common.Dtos.SignalR;
using ChatApp.Application.Common.Interfaces;
using ChatApp.Domain.Events;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace ChatApp.SignalR.Hubs
{
    public class MessageHub(IEventPublisher _eventPublisher, ILogger<MessageHub> _logger) : Hub
    {
        //Triggers when user login success
        public override async Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;

            if (Context.UserIdentifier is not null)
            {
                var userId = Convert.ToInt32(Context.UserIdentifier);

                UserConnectedEvent userConnectedEvent = new(userId, connectionId);
                await _eventPublisher.PublishAsync(userConnectedEvent);
                await _eventPublisher.PublishAsync(new UserOnlineStatusChangedEvent(userId, true));

                Console.WriteLine($"Connected : {connectionId}, User Id:{userId}");
            }

            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var connectionId = Context.ConnectionId;
            var userId = Convert.ToInt32(Context.UserIdentifier);

            await _eventPublisher.PublishAsync(new UserDisconnectedEvent(userId, connectionId));
            await _eventPublisher.PublishAsync(new UserOnlineStatusChangedEvent(userId, false));

            await base.OnDisconnectedAsync(exception);
        }
        public async Task SendMessage(MessageSignal message)
        {
            var userId = Convert.ToInt32(Context.UserIdentifier);

            _logger.LogInformation($"User Id : {userId} sended message to {message.ToUserId}. \n\t Message : {message.MessageText}----{DateTime.Now}");
            if (!userId.Equals(message.FromUserId))
            {
                return;
            }

            NewMessageEvent newMessageEvent = new(message.FromUserId,
                message.ToUserId,
                message.MessageText,
                message.Timestamp);

            await _eventPublisher.PublishAsync(newMessageEvent);
        }
        public async Task UserOnlineStatusChanged(bool isWindowVisible)
        {
            var userId = Convert.ToInt32(Context.UserIdentifier);
            await _eventPublisher.PublishAsync(new UserOnlineStatusChangedEvent(userId, isWindowVisible));
        }
    }
}