using ChatApp.Application.Common.Dtos.User;
using MediatR;

namespace ChatApp.Application.User.Queries.GetById
{
    public record GetUserByIdQuery(int Id) : IRequest<UserDto>
    {
    }
}
