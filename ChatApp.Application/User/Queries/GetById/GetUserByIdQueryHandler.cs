using ChatApp.Application.Common.Dtos.User;
using ChatApp.Application.Common.Interfaces.Repository;
using MediatR;

namespace ChatApp.Application.User.Queries.GetById
{
    internal class GetUserByIdQueryHandler(IUserRepository _userRepository) : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
            if (user is null)
                throw new NullReferenceException(nameof(user));

            return new UserDto(user.Id, user.Username, "C:\\Users\\mehme\\Downloads\\profilePic.jpg", user.LastSeen);
        }
    }
}
