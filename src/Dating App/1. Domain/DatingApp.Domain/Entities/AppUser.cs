﻿using DatingApp.Domain.Entities.Enums;

namespace DatingApp.Domain.Entities
{
    public class AppUser
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Name { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public GenderType Gender { get; set; }

        public string Bio { get; set; }

        public string LookingFor { get; set; }

        public string Interests { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastActive { get; set; }

        public DateTime DateOfBirth { get; set; }

        public ICollection<Photo> Photos { get; set; }
    }
}
