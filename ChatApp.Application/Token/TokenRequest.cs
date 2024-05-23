using MediatR;

namespace ChatApp.Application.Token
{
    public class TokenRequest(string id, string username, string email) : IRequest<string>
    {
        public string Id { get; set; } = id;
        public string Username { get; set; } = username;
        public string Email { get; set; } = email;
    }
}
