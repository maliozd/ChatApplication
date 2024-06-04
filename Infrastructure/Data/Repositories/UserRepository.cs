using ChatApp.Application.Common.Interfaces.Repository;
using ChatApp.Domain.Entities;

namespace Infrastructure.Data.Repositories
{
    public class UserRepository(AppDbContext _dbContext) : IUserRepository
    {
        public async Task<int> AddAsync(AppUser user, CancellationToken cancellationToken)
        {
            await _dbContext.AddAsync(user, cancellationToken);
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public Task<IEnumerable<AppUser>> GetAllAsync()
        {
            return Task.Run(() => _dbContext.Users.AsEnumerable());
        }

        public async Task<AppUser?> GetByIdAsync(int userId, CancellationToken cancellationToken)
        {
            return await _dbContext.Users.FindAsync(userId, cancellationToken);
        }

        public Task<AppUser> GetByUsernameOrEmailAsync(string usernameOrEmail)
        {
            AppUser user = _dbContext.Users.Where(user => user.Email == usernameOrEmail).FirstOrDefault();
            user ??= _dbContext.Users.Where(user => user.Username == usernameOrEmail).FirstOrDefault();
            return Task.FromResult(user);
        }

        public Task<AppUser?> GetWhere(Func<AppUser, bool> predicate)
        {
            AppUser? user = _dbContext.Users.Where(predicate).FirstOrDefault();
            return Task.FromResult(user);
        }

        public async Task<int> UpdateAsync(AppUser user, CancellationToken cancellationToken)
        {
            _dbContext.Update(user);
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
