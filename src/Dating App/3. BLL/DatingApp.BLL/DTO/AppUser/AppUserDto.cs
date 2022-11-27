using DatingApp.BLL.DTO.Photo;
using DatingApp.Domain.Entities.Enums;

namespace DatingApp.BLL.DTO.AppUser
{
    public class AppUserDto
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Name { get; set; }

        public string PhotoUrl { get; set; }

        public int Age { get; set; }

        public string Bio { get; set; }

        public string LookingFor { get; set; }

        public string Interests { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastActive { get; set; }

        public ICollection<PhotoReadDto> Photos { get; set; }
    }
}
