using DatingApp.Domain.Entities.Enums;

namespace DatingApp.BLL.DTO
{
    public class AppUserUpdateDto
    {
        public string Name { get; set; }

        public string Bio { get; set; }

        public string LookingFor { get; set; }

        public string Interests { get; set; }

        public string City { get; set; }

        public string Region { get; set; }
    }
}
