using AspNetCoreHero.ToastNotification.Abstractions;
using DatingApp.BLL.DTO.AppUser;
using DatingApp.BLL.Services.TokenService;
using DatingApp.BLL.Services.UserService;
using DatingApp.Domain.Constants;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.WebApi.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService; 
        private readonly ILogger<AccountController> _logger;
        private readonly INotyfService _toastNotification;

        public AccountController(IUserService userService, 
            ITokenService tokenService, 
            ILogger<AccountController> logger,
            INotyfService sideNotification)
        {
            _userService = userService;
            _tokenService = tokenService; 
            _logger = logger;
            _toastNotification = sideNotification;
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AppUserLoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _userService.LoginAppUser(loginDto);

            if (!result.Success)
            {
                _toastNotification.Error(result.Error);
                ModelState.Clear();

                return View();
            }

            if (!Request.Query.Keys.Contains("ReturnUrl"))
            {
                _toastNotification.Success(Notifications.Successful);

                return RedirectToAction("GetUsers", "AppUsers");
            }            

            return Redirect(Request.Query["ReturnUrl"].First());            
        }

        //[HttpGet]
        //public async Task<IActionResult> Logout()
        //{
        //    await _signInManager.SignOutAsync();
        //    return RedirectToAction("Index", "App");
        //}

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AppUserRegisterDto registerDto)
        {
            var appUserExists = await _userService.IsAppUserExists(registerDto.Username);

            if (appUserExists.Success)
            {
                _toastNotification.Error(Errors.AppUserExists);
                ModelState.Clear();

                return View();
            }

            var result = await _userService.AddAppUser(registerDto);

            if (!result.Success)
            {
                _toastNotification.Error(result.Error);
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
                _toastNotification.Success(Notifications.Successful);

                return RedirectToAction("Home", "App");
            }

            return Redirect(Request.Query["ReturnUrl"].First());
        }

        //[HttpPut]
        //public async Task<ActionResult> EditProfile()
        //{

        //}

        [HttpGet("{action}")]
        public IActionResult Cancel()
        {
            return RedirectToAction("Home", "App");
        }
    }
}