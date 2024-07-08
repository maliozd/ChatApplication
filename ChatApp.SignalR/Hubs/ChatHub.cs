using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace ChatApp.SignalR.Hubs
{
    //[Authorize] https://www.youtube.com/shorts/WtcH_M3hoHs
    public class ChatHub : Hub
    {
        private static readonly ConcurrentDictionary<string, string> _userConnections = new ConcurrentDictionary<string, string>();

        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;
            var connectionId = Context.ConnectionId;

            if (!string.IsNullOrEmpty(userId))
            {
                _userConnections[userId] = connectionId;
            }
            Console.WriteLine("Connected");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.UserIdentifier;
            var a = Context.Items;
            if (!string.IsNullOrEmpty(userId))
            {
                _userConnections.TryRemove(userId, out _);
            }

            await base.OnDisconnectedAsync(exception);
        }

    }
}
