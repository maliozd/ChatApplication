namespace ChatApp.Application.Common.Dtos.Message
{
    public record ChatMessageDto(int Id, string MessageText, int FromUserId, int ToUserId, DateTime Timestamp)
    {
        //public int Id { get; set; }
        //public string MessageText { get; set; }
        //public DateTime Timestamp { get; set; }
        //public UserDto FromUser { get; set; }
        //public UserDto ToUser { get; set; }
    }
}
