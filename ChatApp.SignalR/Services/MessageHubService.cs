using Application.Common.Interfaces.Hubs;
using ChatApp.Application.Common.Constants;
using ChatApp.Application.Common.Dtos.SignalR;
using ChatApp.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.SignalR.Services
{
    public class MessageHubService(IHubContext<MessageHub> _hubContext) : IMessageHubService
    {
        public async Task SendMessageAsync(MessageSignal chatMessage)
        {
            await _hubContext.Clients.User(chatMessage.ToUserId.ToString()).SendAsync(SignalRConstants.ReceiveMessageFunctionName, chatMessage);
        }
        public async Task ChangeOnlineStatusAsync(string[] connectionIds, string userId, bool onlineStatus)
        {
            await _hubContext.Clients.AllExcept(connectionIds).SendAsync(SignalRConstants.UserOnlineStatusChangedFunctionName, userId, onlineStatus);
        }
    }
    //public class HubService : IHubContext<MessageHub>
    //{
    //    public IHubClients Clients => Clients.

    //    public IGroupManager Groups => throw new NotImplementedException();
    //}

}
