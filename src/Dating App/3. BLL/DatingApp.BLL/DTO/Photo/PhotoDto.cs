﻿namespace DatingApp.BLL.DTO.Photo
{
    public class PhotoDto
    {
        public int Id { get; set; }

        public string PublicId { get; set; }

        public string Url { get; set; }

        public bool IsMain { get; set; }

        public int OwnerId { get; set; }
    }
}