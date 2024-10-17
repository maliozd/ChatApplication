using MediatR;

namespace ChatApp.Domain.Events
{
    public record UserDisconnectedEvent(int UserId, string ConnectionId) : INotification;

}
