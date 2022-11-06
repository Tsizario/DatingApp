namespace DatingApp.Domain.Entities
{
    public class UserLike
    {

        public int AuthorId { get; set; }

        public int LikedUserId { get; set; }

        public AppUser LikedUser { get; set; }

        public AppUser Author { get; set; }
    }
}
