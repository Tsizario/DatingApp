using AspNetCoreHero.ToastNotification.Abstractions;
using DatingApp.BLL.DTO.AppUser;
using DatingApp.BLL.DTO.Photo;
using DatingApp.BLL.Services.CloudPhotoService;
using DatingApp.BLL.Services.PhotoService;
using DatingApp.BLL.Services.TokenService;
using DatingApp.BLL.Services.UserService;
using DatingApp.Domain.Constants;
using DatingApp.UI.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.WebApi.Controllers
{
    [Route("users")]
    public class AppUsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly INotyfService _toastNotification;
        private readonly ICloudPhotoService _cloudPhotoService;
        private readonly IPhotoService _photoService;

        public AppUsersController(IUserService userService,
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

        [HttpGet]
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

        [HttpGet("{username}")]
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

        [HttpGet("{username}/edit")]
        public async Task<ActionResult> Edit(string username)
        {
            var user = await _userService.GetAppUserByUsername(username);

            if (!user.Success)
            {
                _toastNotification.Error(user.Error);

                return RedirectToAction("GetUsers");
            }

            return View(user.Value);
        }

        [HttpPost("{username}/edit")]
        public async Task<ActionResult> Edit(AppUserDto updateUser)
        {
            var user = await _userService.UpdateAppUser(updateUser);

            if (!user.Success)
            {
                _toastNotification.Error(user.Error);

                return RedirectToAction("Edit", user.Value);
            }

            _toastNotification.Success(Notifications.AppUserProfileUpdated);

            return View(updateUser);
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoReadDto>> AddPhoto(IFormFile file, string username)
        {
            var user = await _userService.GetAppUserByUsername(username);

            if (!user.Success)
            {
                _toastNotification.Error(user.Error);

                return RedirectToAction("Edit", user.Value);
            }

            var result = await _cloudPhotoService.AddPhotoAsync(file);

            if (result.Error != null)
            {
                _toastNotification.Error(result.Error.Message);

                return RedirectToAction("Edit", user.Value);
            }

            var photoDto = new PhotoDto
            {
                PublicId = result.PublicId,
                Url = result.SecureUrl.AbsoluteUri,
                OwnerId = user.Value.Id
            };

            if (user.Value.Photos.Count == 0)
                photoDto.IsMain = true;

            var addedPhoto = await _photoService.AddPhoto(photoDto);

            if (!addedPhoto.Success)
            {
                _toastNotification.Error(addedPhoto.Error);

                return RedirectToAction("Edit", user.Value);
            }

            _toastNotification.Success(Notifications.Successful);

            return View("Edit", user.Value);
        }
    }
}