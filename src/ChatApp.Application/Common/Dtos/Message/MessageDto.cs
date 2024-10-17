namespace ChatApp.Application.Common.Dtos.Message
{
    public record MessageDto(int Id, string MessageText, string FromName, int FromId, bool ReadStatus, DateTime
        Timestamp)
    {

    }
}
