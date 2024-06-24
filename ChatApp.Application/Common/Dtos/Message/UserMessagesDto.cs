namespace ChatApp.Application.Common.Dtos.Message
{
    public record UserMessagesDto(int UserId, List<ChatMessageDto> Messages)
    {
        //public int Id { get; set; }
        //public List<ChatMessageDto> Messages { get; set; }
    }
    public record MessagesDto(List<UserMessagesDto> Messages)
    {

    }
}
