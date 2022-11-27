using DatingApp.BLL.DTO;
using DatingApp.BLL.DTO.Photo;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.UI.Components
{
    public class UserPhotoViewComponent : ViewComponent
    {
        public UserPhotoViewComponent()
        {
        }

        public IViewComponentResult Invoke(PhotoReadDto photoDto)
        {
            return View(photoDto);
        }
    }
}
