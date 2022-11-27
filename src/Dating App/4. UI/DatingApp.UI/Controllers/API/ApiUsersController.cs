using AspNetCoreHero.ToastNotification.Abstractions;
using DatingApp.BLL.DTO.AppUser;
using DatingApp.BLL.DTO.Photo;
using DatingApp.BLL.Services.CloudPhotoService;
using DatingApp.BLL.Services.PhotoService;
using DatingApp.BLL.Services.TokenService;
using DatingApp.BLL.Services.UserService;
using DatingApp.Domain.Constants;
using DatingApp.UI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.UI.Controllers.API
{
    [ApiController]
    [Route("api/users")]
    public class ApiUsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly INotyfService _toastNotification;
        private readonly ICloudPhotoService _cloudPhotoService;
        private readonly IPhotoService _photoService;

        public ApiUsersController(IUserService userService,
            ITokenService tokenService,
            INotyfService toastNotification,
            ICloudPhotoService cloudPhotoService,
            IPhotoService photoService)
        {
            _userService = userService;
            _tokenService = tokenService;
            _toastNotification = toastNotification;
            _cloudPhotoService = cloudPhotoService;
            _photoService = photoService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AppUserTokenDto>> Register(AppUserRegisterDto appUserDto)
        {
            var userExists = await _userService.IsAppUserExists(appUserDto.Username);

            if (userExists.Success)
                return BadRequest(Errors.AppUserExists);

            var result = await _userService.AddAppUser(appUserDto);

            if (!result.Success)
                return BadRequest(result.Error);

            var appUserTokenDto = new AppUserTokenDto
            {
                Username = result.Value.Username,
                Token = _tokenService.CreateToken(result.Value)
            };

            return CreatedAtAction(nameof(Register), appUserTokenDto);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AppUserTokenDto>> Login(AppUserLoginDto loginDto)
        {
            var result = await _userService.LoginAppUser(loginDto);

            if (!result.Success)
                return Unauthorized(result.Error);

            var appUserTokenDto = new AppUserTokenDto
            {
                Username = result.Value.Username,
                Token = _tokenService.CreateToken(result.Value)
            };

            return Ok(appUserTokenDto);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUserDto>>> GetUsers()
        {
            var users = await _userService.GetAllAppUsers();

            if (!users.Success)
                return NotFound(users.Error);

            return Ok(users.Value);
        }


        [Authorize]
        [HttpGet("{username}")]
        public async Task<ActionResult<AppUserDto>> GetUser(string username)
        {
            var user = await _userService.GetAppUserByUsername(username);

            if (!user.Success)
                return NotFound(user.Error);

            return Ok(user.Value);
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoReadDto>> AddPhoto(IFormFile file)
        {
            var user = await _userService.GetAppUserByUsername(User.GetUserName());

            if (!user.Success)
            {
                return NotFound(user.Error);
            }

            var result = await _cloudPhotoService.AddPhotoAsync(file);

            if (result.Error != null)
            {
                return NotFound(result.Error.Message);
            }

            var photoDto = new PhotoDto
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
                OwnerId = user.Value.Id
            };

            if (user.Value.Photos.Count == 0)
                photoDto.IsMain = true;

            var addedPhoto = await _photoService.AddPhoto(photoDto);

            if (!addedPhoto.Success)
            {
                return BadRequest(addedPhoto.Error);
            }

            return addedPhoto.Value;
        }
    }
}
