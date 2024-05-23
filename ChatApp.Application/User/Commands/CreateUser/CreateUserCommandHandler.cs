using ChatApp.Application.Common.Interfaces.Repository;
using ChatApp.Domain.Entities;
using MediatR;
using System.Security.Cryptography;
using System.Text;

namespace ChatApp.Application.User.Commands.CreateUser
{
    public class CreateUserCommandHandler(IUserRepository _userRepository) : IRequestHandler<CreateUserCommand, int>
    {
        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetWhere(u => u.Email == request.Email || u.Username == request.Username);
            if (existingUser != null)
            {
                throw new Exception("User already exists!");
            }

            using HMACSHA512 hmac = new();
            byte[] passwordSalt = hmac.Key;
            byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));

            AppUser newUser = new()
            {
                Email = request.Email,
                Username = request.Username,
                IsEmailConfirmed = false,
                IP = request.IPAddress,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,

            };
            return await _userRepository.AddAsync(newUser, cancellationToken);
        }
    }
}
