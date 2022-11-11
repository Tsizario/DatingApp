using BLL.Helpers;
using DatingApp.BLL.DTO;
using DatingApp.Domain.Entities;

namespace DatingApp.BLL.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResult<AppUserDto>> GetAppUserByUsername(string username);

        Task<ServiceResult<IEnumerable<AppUserDto>>> GetAllAppUsers();

        Task<ServiceResult<AppUser>> AddAppUser(AppUserRegisterDto appUserDto);

        Task<ServiceResult<AppUser>> LoginAppUser(AppUserLoginDto loginDto);

        Task<ServiceResult<bool>> IsAppUserExists(string username);
    }
}
