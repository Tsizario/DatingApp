using DatingApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.DAL.Repositories.UserRepository
{
    internal class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AppUser> GetUserByIdAsync(int userId)
        {
            return await _dbContext.Users.FindAsync(userId);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _dbContext.Users
                .SingleOrDefaultAsync(user => user.Username == username);
        }

        public async Task<IEnumerable<AppUser>> GetAllUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<AppUser> AddUserAsync(AppUser appUser)
        {
            await _dbContext.Users.AddAsync(appUser);

            await _dbContext.SaveChangesAsync();

            return appUser;
        }

        public async Task<bool> ExistsAsync(string username)
        {
            return await _dbContext.Users
                .AnyAsync(user => user.Username == username.ToLower());
        }       
    }
}
