using ChatApp.Application.Common.Interfaces;
using ChatApp.Domain.Events;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.SignalR.Hubs
{
    public class MessageHub(IConnectionPool cache, IEventPublisher eventPublisher) : Hub
    {
        readonly IConnectionPool _cache = cache;
        readonly IEventPublisher _eventPublisher = eventPublisher;

        public override async Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;

            if (Context.UserIdentifier is not null)
            {
                var userId = Convert.ToInt32(Context.UserIdentifier);

                UserConnectedEvent userConnectedEvent = new(userId, connectionId);
                await _eventPublisher.PublishAsync(userConnectedEvent);

                Console.WriteLine($"Connected : {connectionId}, User Id:{userId}");
            }

            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var connectionId = Context.ConnectionId;
            var userId = Convert.ToInt32(Context.UserIdentifier);

            await _eventPublisher.PublishAsync(new UserDisconnectedEvent(userId, connectionId));

            await base.OnDisconnectedAsync(exception);
        }
    }
}