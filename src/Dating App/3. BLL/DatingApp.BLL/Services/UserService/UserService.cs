using AutoMapper;
using BLL.Helpers;
using DatingApp.BLL.DTO;
using DatingApp.DAL.Repositories.UserRepository;
using DatingApp.Domain.Constants;

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

        public async Task<ServiceResult<bool>> IsAppUserExists(string username)
        {
            var appUserExists = await _userRepository.ExistsAsync(username);

            return appUserExists == true
                ? ServiceResult<bool>.CreateSuccess(appUserExists)
                : ServiceResult<bool>.CreateFailure(Errors.AppUserExists);
        }
    }
}
