using System.ComponentModel.DataAnnotations;

namespace DatingApp.BLL.DTO
{
    public class AppUserRegisterDto
    {
        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(15, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
