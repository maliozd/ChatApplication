using ChatApp.Application.Common.Interfaces.Repository;
using ChatApp.Application.Token;
using MediatR;
using System.Text;

namespace ChatApp.Application.User.Commands.LoginUser
{
    public class LoginUserCommandHandler(IUserRepository _userRepository, IMediator _mediator) : IRequestHandler<LoginUserCommand, string>
    {
        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUsernameOrEmailAsync(request.UsernameOrEmail);

            if (user is null)
                throw new Exception("Email or username is wrong");

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                throw new Exception("Password is wrong");

            string token = await _mediator.Send(new TokenRequest(user.Id.ToString(), user.Username, user.Email), cancellationToken);

            return token;
        }
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }

        }
    }
}
