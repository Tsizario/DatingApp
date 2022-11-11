using DatingApp.BLL.DTO;
using DatingApp.BLL.Services.TokenService;
using DatingApp.BLL.Services.UserService;
using DatingApp.Domain.Constants;
using DatingApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AccountController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AppUserTokenDto>> Register(AppUserRegisterDto registerDto)
        {
            var appUserExists = await _userService.IsAppUserExists(registerDto.Username);

            if (appUserExists.Success)
                return BadRequest(Errors.AppUserExists);

            var result = await _userService.AddAppUser(registerDto);

            if (!result.Success)
                return BadRequest(result.Error);

            var appUserTokenDto = new AppUserTokenDto
            {
                Username = registerDto.Username,
                Token = _tokenService.CreateToken(result.Value)
            };

            return CreatedAtAction(nameof(Register), appUserTokenDto);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AppUserTokenDto>> Login(AppUserLoginDto loginDto)
        {
            var appUser = await _userService.GetAppUserByUsername(loginDto.Username);

            if (!appUser.Success)
                return NotFound(appUser.Error);

            var result = await _userService.LoginAppUser(loginDto);

            if (!result.Success)
                return Unauthorized(result.Error);

            var appUserTokenDto = new AppUserTokenDto
            {
                Username = loginDto.Username,
                Token = _tokenService.CreateToken(result.Value)
            };

            return Ok(appUserTokenDto);
        }
    }
}
