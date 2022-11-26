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
            var appUser = await _userRepository.GetByUsernameAsync(username);

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

            appUser = await _userRepository.AddAsync(appUser);

            return appUser is not null
                ? ServiceResult<AppUser>.CreateSuccess(appUser)
                : ServiceResult<AppUser>.CreateFailure(Errors.AppUserAddingError);
        }
        public async Task<ServiceResult<bool>> UpdateAppUserByUsername(AppUserUpdateDto updatedDto)
        {
            var updatedAppUser = _mapper.Map<AppUser>(updatedDto);

            var isUpdatedUser = await _userRepository.UpdateAsync(updatedAppUser);

            return isUpdatedUser
                ? ServiceResult<bool>.CreateSuccess(isUpdatedUser)
                : ServiceResult<bool>.CreateFailure(Errors.AppUserNotFound);
        }
        public async Task<ServiceResult<AppUser>> LoginAppUser(AppUserLoginDto loginDto)
        {
            var appUser = await _userRepository.GetByUsernameAsync(loginDto.Username);

            if (appUser is null)
                return ServiceResult<AppUser>.CreateFailure(Errors.AppUserIncorrectCredentials);

            using var hmac = new HMACSHA256(appUser.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            bool isEqual = computedHash.SequenceEqual(appUser.PasswordHash);

            return isEqual == true
                ? ServiceResult<AppUser>.CreateSuccess(appUser)
                : ServiceResult<AppUser>.CreateFailure(Errors.AppUserIncorrectCredentials);                
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
