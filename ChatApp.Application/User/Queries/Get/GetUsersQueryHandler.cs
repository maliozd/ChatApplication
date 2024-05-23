using ChatApp.Application.Common.Dtos.User;
using ChatApp.Application.Common.Interfaces.Repository;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ChatApp.Application.User.Queries.Get
{
    public class GetUsersQueryHandler(IUserRepository _userRepository, IHttpContextAccessor _httpContextAccessor) : IRequestHandler<GetUsersQuery, List<UserDto>>
    {

        public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var requestUserId = _httpContextAccessor.HttpContext.User;
            var users = await _userRepository.GetAllAsync();
            return users.Select(user => new UserDto(user.Id, user.Username)).ToList();
        }
    }
}
