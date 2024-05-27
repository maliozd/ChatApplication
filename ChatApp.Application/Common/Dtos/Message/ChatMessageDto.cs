using ChatApp.Application.Common.Dtos.User;

namespace ChatApp.Application.Common.Dtos.Message
{
    public record ChatMessageDto(int Id, string MessageText, UserDto FromUser, UserDto ToUser, DateTime Timestamp)
    {
        //public int Id { get; set; }
        //public string MessageText { get; set; }
        //public DateTime Timestamp { get; set; }
        //public UserDto FromUser { get; set; }
        //public UserDto ToUser { get; set; }
    }
}
