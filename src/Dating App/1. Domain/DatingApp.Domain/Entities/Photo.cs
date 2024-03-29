﻿using System.ComponentModel.DataAnnotations.Schema;

namespace DatingApp.Domain.Entities
{
    [Table("Photos")]
    public class Photo
    {
        public int Id { get; set; }
        
        public string PublicId { get; set; }

        public string Url { get; set; }

        public bool IsMain { get; set; }

        public int OwnerId { get; set; }

        public AppUser Owner { get; set; }
    }
}
