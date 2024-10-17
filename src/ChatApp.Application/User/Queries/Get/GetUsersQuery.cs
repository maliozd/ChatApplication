using ChatApp.Application.Common.Dtos.User;
using MediatR;

namespace ChatApp.Application.User.Queries.Get
{
    public record GetUsersQuery() : IRequest<List<UserDto>>
    {
    }
}
