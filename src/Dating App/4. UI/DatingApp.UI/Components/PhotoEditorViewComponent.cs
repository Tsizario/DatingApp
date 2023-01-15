using DatingApp.BLL.DTO.AppUser;
using DatingApp.BLL.DTO.Photo;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.UI.Components
{
    public class PhotoEditorViewComponent : ViewComponent
    {
        public PhotoEditorViewComponent()
        {
        }

        public IViewComponentResult Invoke(AppUserDto userDto)
        {
            return View(userDto);
        }
    }
}