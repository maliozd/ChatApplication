using ChatApp.Application.Common.Interfaces;
using ChatApp.Domain.Events;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.SignalR.Hubs
{
    public class MessageHub : Hub
    {

        readonly IConnectionPool _cache;
        readonly IEventPublisher _eventPublisher;
        public MessageHub(IConnectionPool cache, IEventPublisher eventPublisher)
        {
            _cache = cache;
            _eventPublisher = eventPublisher;
        }

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
