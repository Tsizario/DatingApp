using AutoMapper;
using BLL.Helpers;
using DatingApp.BLL.DTO;
using DatingApp.DAL.Repositories.UserRepository;
using DatingApp.Domain.Constants;
using DatingApp.Domain.Entities;
using System.Security.Cryptography;
using System.Text;

namespace DatingApp.BLL.Services.UserService
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository,
            IMapper mapper)
        {
            _userRepository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<IEnumerable<AppUserDto>>> GetAllAppUsers()
        {
            var appUsers = await _userRepository.GetAllUsersAsync();
            var appUserDtos = _mapper.Map<IEnumerable<AppUserDto>>(appUsers);

            return appUserDtos is not null
                ? ServiceResult<IEnumerable<AppUserDto>>.CreateSuccess(appUserDtos)
                : ServiceResult<IEnumerable<AppUserDto>>.CreateFailure(Errors.AppUsersNotFound);
        }

        public async Task<ServiceResult<AppUserDto>> GetAppUserByUsername(string username)
        {
            var appUser = await _userRepository.GetUserByUsernameAsync(username);

            var appUserDto = _mapper.Map<AppUserDto>(appUser);

            return appUserDto is not null
                ? ServiceResult<AppUserDto>.CreateSuccess(appUserDto)
                : ServiceResult<AppUserDto>.CreateFailure(Errors.AppUserNotFound);
        }

        public async Task<ServiceResult<AppUser>> AddAppUser(AppUserRegisterDto registerDto)
        {
            var appUser = _mapper.Map<AppUser>(registerDto);

            using var hmac = new HMACSHA256();

            appUser.Username = registerDto.Username.ToLower();
            appUser.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));
            appUser.PasswordSalt = hmac.Key;

            appUser = await _userRepository.AddUserAsync(appUser);

            return appUser is not null
                ? ServiceResult<AppUser>.CreateSuccess(appUser)
                : ServiceResult<AppUser>.CreateFailure(Errors.AppUserAddingError);
        }

        public async Task<ServiceResult<AppUser>> LoginAppUser(AppUserLoginDto loginDto)
        {
            var appUser = await _userRepository.GetUserByUsernameAsync(loginDto.Username);

            using var hmac = new HMACSHA256(appUser.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            bool isFailed = false;

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != appUser.PasswordHash[i])
                {
                    isFailed = true;
                    break;
                }
            }

            return isFailed == true
                ? ServiceResult<AppUser>.CreateFailure(Errors.AppUserPasswordInvalid)
                : ServiceResult<AppUser>.CreateSuccess(appUser);
        }

        public async Task<ServiceResult<bool>> IsAppUserExists(string username)
        {
            var appUserExists = await _userRepository.ExistsAsync(username);

            return appUserExists == true
                ? ServiceResult<bool>.CreateSuccess(appUserExists)
                : ServiceResult<bool>.CreateFailure(Errors.AppUserNotFound);
        }
    }
}
