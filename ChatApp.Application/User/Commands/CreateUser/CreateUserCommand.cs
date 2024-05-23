using MediatR;

namespace ChatApp.Application.User.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<int>
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string IPAddress { get; set; }
    }
}
