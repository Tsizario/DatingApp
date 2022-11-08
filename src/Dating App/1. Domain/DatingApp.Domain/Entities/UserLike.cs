﻿namespace DatingApp.Domain.Entities
{
    public class UserLike
    {

        public int SourceUserId { get; set; }

        public int LikedUserId { get; set; }

        public AppUser LikedUser { get; set; }

        public AppUser SourceUser { get; set; }
    }
}
