using DatingApp.BLL.DTO;
using DatingApp.BLL.Services.TokenService;
using DatingApp.BLL.Services.UserService;
using DatingApp.Domain.Constants;
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

        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(AppUserLoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _userService.LoginAppUser(loginDto);

            if (!result.Success)
            {
                TempData["Error"] = result.Error;
                ModelState.Clear();

                return View();
            }

            if (!Request.Query.Keys.Contains("ReturnUrl"))
            {
                return RedirectToAction("Start", "App");
            }

            return Redirect(Request.Query["ReturnUrl"].First());            
        }

        //[HttpGet]
        //public async Task<IActionResult> Logout()
        //{
        //    await _signInManager.SignOutAsync();
        //    return RedirectToAction("Index", "App");
        //}

        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(AppUserRegisterDto registerDto)
        {
            var appUserExists = await _userService.IsAppUserExists(registerDto.Username);

            if (appUserExists.Success)
            {
                TempData["Error"] = Errors.AppUserExists;
                ModelState.Clear();
                return View();
            }

            var result = await _userService.AddAppUser(registerDto);

            if (!result.Success)
            {
                TempData["Error"] = result.Error;
                ModelState.Clear();
                return View();
            }

            var appUserTokenDto = new AppUserTokenDto
            {
                Username = registerDto.Username,
                Token = _tokenService.CreateToken(result.Value)
            };

            if (!Request.Query.Keys.Contains("ReturnUrl"))
            {
                return RedirectToAction("Start", "App");
            }

            return Redirect(Request.Query["ReturnUrl"].First());
        }

        [HttpGet]
        public IActionResult Cancel()
        {
            return RedirectToAction("Start", "App");
        }
    }
}
