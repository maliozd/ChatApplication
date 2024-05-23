using ChatApp.Application.Common.Dtos.SignalR;

namespace Application.Common.Interfaces.Hubs
{
    public interface IMessageHubService
    {
        public Task SendMessageAsync(MessageDto chatMessage);
        Task SendMessageAsync(MessageDto chatMessage, string connectionId);
        public Task SendMessageAsync(MessageDto chatMessage, string userId, string function);
    }
}