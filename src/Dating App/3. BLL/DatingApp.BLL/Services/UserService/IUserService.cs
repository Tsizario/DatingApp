using BLL.Helpers;
using DatingApp.BLL.DTO.AppUser;
using DatingApp.Domain.Entities;

namespace DatingApp.BLL.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResult<IEnumerable<AppUserDto>>> GetAllAppUsers();

        Task<ServiceResult<AppUserDto>> GetAppUserByUsername(string username);

        Task<ServiceResult<AppUserDto>> GetAppUserById(int id);

        Task<ServiceResult<AppUser>> AddAppUser(AppUserRegisterDto appUserDto);

        Task<ServiceResult<AppUserDto>> UpdateAppUser(AppUserDto updatedDto);

        Task<ServiceResult<AppUser>> LoginAppUser(AppUserLoginDto loginDto);

        Task<ServiceResult<bool>> IsAppUserExists(string username);
    }
}
