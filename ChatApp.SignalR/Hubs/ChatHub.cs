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

        public async Task SendPrivateMessage(string toUserId, string fromUserId, string messageText)
        {
            var asd = Context.UserIdentifier;

            if (fromUserId == null)
            {
                // Handle unauthenticated user
                return;
            }

            //var command = new SendPrivateMessageCommand
            //{
            //    FromUserId = int.Parse(fromUserId),
            //    ToUserId = int.Parse(toUserId),
            //    MessageText = messageText
            //};

            //await _mediator.Send(command);

            if (_userConnections.TryGetValue(toUserId, out var connectionId))
            {
                await Clients.Client(connectionId).SendAsync("ReceivePrivateMessage", fromUserId, messageText);
            }
            else
            {
                // Push notification logic for offline users
                //await _pushNotificationService.SendPushNotificationAsync(toUserId, messageText);
            }
        }
    }
}
