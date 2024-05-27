using ChatApp.Application.Common.Dtos.SignalR;

namespace Application.Common.Interfaces.Hubs
{
    public interface IMessageHubService
    {
        public Task SendMessageAsync(MessageSignal chatMessage);
        Task SendMessageAsync(MessageSignal chatMessage, string connectionId);
        public Task SendMessageAsync(MessageSignal chatMessage, string userId, string function);
    }
}