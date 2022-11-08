using DatingApp.Domain.Entities;

namespace DatingApp.DAL.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<AppUser> GetUserByIdAsync(int id);

        Task<AppUser> GetUserByUsernameAsync(string username);

        Task<IEnumerable<AppUser>> GetAllUsersAsync();

        Task<AppUser> AddUserAsync(AppUser addedEntity);

        Task<bool> ExistsAsync(string username);
    }
}
