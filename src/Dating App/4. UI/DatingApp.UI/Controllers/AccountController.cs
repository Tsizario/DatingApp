using DatingApp.BLL.DTO;
using DatingApp.BLL.Services.TokenService;
using DatingApp.BLL.Services.UserService;
using DatingApp.Domain.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.WebApi.Controllers
{
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

        [HttpGet("login")]
        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "App"); //Index action of App controller
            }

            return await Task.Run(() => View());
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AppUserLoginDto loginDto)
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

            return View(loginDto);
        }

        //[HttpGet]
        //public async Task<IActionResult> Logout()
        //{
        //    await _signInManager.SignOutAsync();
        //    return RedirectToAction("Index", "App");
        //}

        [HttpGet("register")]
        public async Task<IActionResult> Register()
        {
            return await Task.Run(() => View());
        }

        [HttpPost]
        public async Task<IActionResult> Register(AppUserRegisterDto registerDto)
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
    }
}
