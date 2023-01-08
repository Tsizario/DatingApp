using DatingApp.BLL.DTO.AppUser;
using DatingApp.BLL.DTO.Photo;

namespace DatingApp.UI.ViewModels
{
    public class AppUserViewModel
    {
        public AppUserDto ViewUser { get; set; }
        public IEnumerable<PhotoDto> ViewPhotos { get; set; }
    }
}
