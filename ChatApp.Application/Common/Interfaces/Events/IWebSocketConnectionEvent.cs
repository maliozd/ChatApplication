namespace ChatApp.Application.Common.Interfaces.Events
{
    public interface IWebSocketConnectionEvent
    {
        int UserId { get; }
        string ConnectionId { get; }
    }
}
