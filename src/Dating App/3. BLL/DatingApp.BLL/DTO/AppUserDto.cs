using DatingApp.Domain.Entities.Enums;

namespace DatingApp.BLL.DTO
{
    public class AppUserDto
    {
        public string Username { get; set; }

        public string PhotoUrl { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public GenderType Gender { get; set; }

        public string Bio { get; set; }

        public string LookingFor { get; set; }

        public string Interests { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastActive { get; set; }

        public DateTime DateOfBirth { get; set; }

        public ICollection<PhotoDto> Photos { get; set; }
    }
}
