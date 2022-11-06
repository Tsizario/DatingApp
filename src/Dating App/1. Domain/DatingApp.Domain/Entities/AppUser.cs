using DatingApp.Domain.Entities.Enums;

namespace DatingApp.Domain.Entities
{
    public class AppUser
    {
        public int Id { get; set; }

        public RoleType Role { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public GenderType Gender { get; set; }

        public string UserName { get; set; }

        public string Bio { get; set; }

        public string LookingFor { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;

        public DateTime LastActive { get; set; } = DateTime.UtcNow;

        public DateTime DateOfBirth { get; set; }

        public List<Interest> Interests { get; set; }

        public ICollection<Photo> Photos { get; set; }

        public ICollection<UserLike> LikesFromUsers { get; set; }

        public ICollection<UserLike> LikesToUsers { get; set; }

        public ICollection<Message> SendMessages { get; set; }

        public ICollection<Message> ReceivedMessages { get; set; }
    }
}
