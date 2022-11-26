using DatingApp.Domain.Entities;

namespace DatingApp.DAL.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<AppUser> GetByIdAsync(int id);

        Task<AppUser> GetByUsernameAsync(string username);

        Task<IEnumerable<AppUser>> GetAllUsersAsync();

        Task<AppUser> AddAsync(AppUser appUser);

        Task<bool> ExistsAsync(string username);
    }
}
