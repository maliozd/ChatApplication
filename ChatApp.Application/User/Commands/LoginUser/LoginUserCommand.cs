using MediatR;

namespace ChatApp.Application.User.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<string>
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
