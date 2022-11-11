using DatingApp.Domain.Entities.Enums;

namespace DatingApp.Domain.Entities
{
    public class AppUser
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
    }
}
