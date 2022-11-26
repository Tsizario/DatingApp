using AspNetCoreHero.ToastNotification.Abstractions;
using DatingApp.BLL.DTO;
using DatingApp.BLL.Services.TokenService;
using DatingApp.BLL.Services.UserService;
using DatingApp.Domain.Constants;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.WebApi.Controllers
{
    public class AppUsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly INotyfService _toastNotification;

        public AppUsersController(IUserService userService,
            ITokenService tokenService,
            INotyfService toastNotification)
        {
            _userService = userService;
            _tokenService = tokenService;
            _toastNotification = toastNotification;
        }

        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<AppUserDto>>> GetUsers()
        {
            var users = await _userService.GetAllAppUsers();

            if (!users.Success)
            {
                _toastNotification.Error(users.Error);

                return View(users.Value);
            }                

            return View("Users", users.Value);
        }

        [HttpGet("users/{username}")]
        public async Task<ActionResult<AppUserDto>> GetUser(string username)
        {
            var user = await _userService.GetAppUserByUsername(username);

            if (!user.Success)
            {
                _toastNotification.Error(user.Error);

                return RedirectToAction("Users");
            }

            return View("User", user.Value);
        }

        [HttpPut("user/edit")]
        public async Task<ActionResult<AppUserUpdateDto>> UpdateUser(AppUserUpdateDto updateUser)
        {
            var user = await _userService.UpdateAppUserByUsername(updateUser);

            if (!user.Success)
            {
                _toastNotification.Error(user.Error);

                return RedirectToAction("UpdateUser");
            }

            _toastNotification.Success(Notifications.AppUserProfileUpdated);

            return View("Edit", user.Value);
        }

        //public async Task<ActionResult> GetImages(string username)
        //{
        //    var photos = await _userService.GetAppUserPhotos(username);

        //    if (!photos.Success)
        //    {

        //    }
        //}
    }
}
