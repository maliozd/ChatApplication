namespace ChatApp.Application.Common.Dtos.SignalR
{
    public record MessageDto(string Message, string From, int FromId, string? ToUserId = null)

    {
    }
}
