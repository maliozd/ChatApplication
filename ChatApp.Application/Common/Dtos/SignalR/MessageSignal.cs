namespace ChatApp.Application.Common.Dtos.SignalR
{
    public record MessageSignal(string Message, string From, int FromId, string? ToUserId = null)

    {
    }
}
