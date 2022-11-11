using System.ComponentModel.DataAnnotations;

namespace DatingApp.BLL.DTO
{
    public class AppUserRegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
