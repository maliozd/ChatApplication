using ChatApp.Application.Common.Interfaces.Events;

namespace ChatApp.SignalR.Events
{
    public record UserConnectedEvent(int UserId, string ConnectionId) : IWebSocketConnectionEvent;
    public record UserDisconnectedEvent(int UserId, string ConnectionId) : IWebSocketConnectionEvent;

}
