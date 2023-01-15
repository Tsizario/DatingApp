using DatingApp.BLL.DTO.AppUser;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.UI.Components
{
    public class UserPhotoViewComponent : ViewComponent
    {
        public UserPhotoViewComponent()
        {
        }

        public IViewComponentResult Invoke(AppUserDto userDto)
        {
            return View(userDto);
        }
    }
}