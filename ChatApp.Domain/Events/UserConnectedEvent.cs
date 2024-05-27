using MediatR;

namespace ChatApp.Domain.Events
{
    public record UserConnectedEvent(int UserId, string ConnectionId) : INotification;
}
