using BLL.Helpers;
using DatingApp.BLL.DTO;

namespace DatingApp.BLL.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResult<AppUserDto>> GetAppUserByUsername(string username);

        Task<ServiceResult<IEnumerable<AppUserDto>>> GetAllAppUsers();
    }
}
