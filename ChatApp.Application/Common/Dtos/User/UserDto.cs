namespace ChatApp.Application.Common.Dtos.User
{
    public record UserDto(int Id, string Username, string ProfilePicturePath, DateTime? LastSeen)
    {

        //public string ProfilePicUrl { get; set; }
    }
}
