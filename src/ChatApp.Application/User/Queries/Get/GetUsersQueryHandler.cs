using ChatApp.Application.Common.Dtos.User;
using ChatApp.Application.Common.Interfaces.Repository;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ChatApp.Application.User.Queries.Get
{
    public class GetUsersQueryHandler(IUserRepository _userRepository, IHttpContextAccessor _httpContextAccessor) : IRequestHandler<GetUsersQuery, List<UserDto>>
    {

        public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var requestUserId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);

            var users = await _userRepository.GetAllAsync();
            var userList = users.ToList();

            var myUser = userList.FirstOrDefault(x => x.Id == requestUserId);
            if (myUser != null)
            {
                userList.Remove(myUser);
            }

            return userList.Select(user => new UserDto(user.Id, user.Username, "C:\\Users\\mehme\\Downloads\\profilePic.jpg", user.LastSeen)).ToList();
        }
    }
}
