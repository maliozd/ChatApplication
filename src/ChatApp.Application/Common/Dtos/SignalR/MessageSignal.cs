namespace ChatApp.Application.Common.Dtos.SignalR
{
    public record MessageSignal(string MessageText, int FromUserId, int ToUserId, DateTime Timestamp);
}
