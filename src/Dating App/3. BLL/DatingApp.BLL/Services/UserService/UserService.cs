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

        public async Task<ServiceResult<IEnumerable<MemberDto>>> GetAllAppUsers()
        {
            var appUsers = await _userRepository.GetAllUsersAsync();
            var appUserDtos = _mapper.Map<IEnumerable<MemberDto>>(appUsers);

            return appUserDtos is not null
                ? ServiceResult<IEnumerable<MemberDto>>.CreateSuccess(appUserDtos)
                : ServiceResult<IEnumerable<MemberDto>>.CreateFailure(Errors.AppUsersNotFound);
        }

        public async Task<ServiceResult<MemberDto>> GetAppUserByUsername(string username)
        {
            var appUser = await _userRepository.GetUserByUsernameAsync(username);

            var appUserDto = _mapper.Map<MemberDto>(appUser);

            return appUserDto is not null
                ? ServiceResult<MemberDto>.CreateSuccess(appUserDto)
                : ServiceResult<MemberDto>.CreateFailure(Errors.AppUserNotFound);
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
