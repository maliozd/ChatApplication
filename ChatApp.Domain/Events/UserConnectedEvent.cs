using MediatR;

namespace ChatApp.Domain.Events
{
    public record UserConnectedEvent(int UserId, string ConnectionId) : INotification;
    public record UserDisconnectedEvent(int UserId, string ConnectionId) : INotification;
    public record NewMessageEvent(int FromUserId, int ToUserId, string MessageText, DateTime Timestamp) : INotification;
    public record UserWindowStateChangedEvent(int UserId, bool IsWindowVisible) : INotification;
}
