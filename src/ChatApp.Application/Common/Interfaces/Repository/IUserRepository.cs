using ChatApp.Domain.Entities;

namespace ChatApp.Application.Common.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<int> AddAsync(AppUser user, CancellationToken cancellationToken);
        Task<AppUser> GetWhere(Func<AppUser, bool> predicate);
        Task<AppUser> GetByUsernameOrEmailAsync(string usernameOrEmail);
        Task<AppUser> GetByIdAsync(int userId, CancellationToken cancellationToken);
        //Task<int> RemoveAsync(AppUser user, CancellationToken cancellationToken);
        Task<int> UpdateAsync(AppUser user, CancellationToken cancellationToken);
        Task<IEnumerable<AppUser>> GetAllAsync();

    }
}
