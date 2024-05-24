namespace ChatApp.Application.Events.Interfaces
{
    public interface IWebSocketConnectionEvent
    {
        int UserId { get; }
        string ConnectionId { get; }
    }
}
