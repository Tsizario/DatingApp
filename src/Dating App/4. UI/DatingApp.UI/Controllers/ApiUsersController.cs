using DatingApp.BLL.DTO;
using DatingApp.BLL.Services.TokenService;
using DatingApp.BLL.Services.UserService;
using DatingApp.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiUsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public ApiUsersController(IUserService userService,
            ITokenService _tokenService)
        {
            _userService = userService;
            _tokenService = _tokenService;
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
    }
}
