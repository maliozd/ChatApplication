using Application.Common.Interfaces.Hubs;
using ChatApp.Application.Common.Dtos.SignalR;
using ChatApp.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.SignalR.Services
{
    public class MessageHubService(IHubContext<MessageHub> _hubContext/*, IConnectionCache _connectionCache*/) : IMessageHubService
    {
        //readonly IConnectionCache _connectionCache;
        public async Task A()
        {

        }
        //MessageReceiveFunctionName
        //in memory cache
        public Task SendMessageAsync(MessageSignal chatMessage)
        {
            _hubContext.Clients.All.SendAsync("MessageReceived", chatMessage);
            return Task.CompletedTask;
        }

        public Task SendMessageAsync(MessageSignal chatMessage, string connectionId)
        {
            _hubContext.Clients.Client(connectionId).SendAsync("MessageReceived", chatMessage);
            return Task.CompletedTask;
        }
        public async Task SendMessageAsync(MessageSignal chatMessage, string connectionId, string function)
        {
            await _hubContext.Clients.Client(connectionId).SendAsync(function, chatMessage);
            //await _hubContext.Clients.Client(connectionId).SendAsync(function, chatMessage);
        }

    }
}
