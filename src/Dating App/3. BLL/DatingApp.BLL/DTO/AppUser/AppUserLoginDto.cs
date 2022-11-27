using System.ComponentModel.DataAnnotations;

namespace DatingApp.BLL.DTO.AppUser
{
    public class AppUserLoginDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
