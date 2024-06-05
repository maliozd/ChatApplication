using ChatApp.Application.Common.Dtos.SignalR;

namespace Application.Common.Interfaces.Hubs
{
    public interface IMessageHubService
    {
        Task SendMessageAsync(MessageSignal chatMessage);
        Task ChangeOnlineStatusAsync(string[] connectionIds, string userId, bool onlineStatus);
    }
}