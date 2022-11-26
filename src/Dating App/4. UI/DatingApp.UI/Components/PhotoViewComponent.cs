using DatingApp.BLL.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.UI.Components
{
    public class PhotoViewComponent : ViewComponent
    {
        public PhotoViewComponent()
        {
        }

        public IViewComponentResult Invoke(PhotoDto photoDto)
        {
            return View(photoDto);
        }
    }
}
