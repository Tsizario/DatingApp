using DatingApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
        : base(options)
        {
        }

        public DbSet<AppUser> Users { get; set; }

        public DbSet<Photo> Photos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AppUser>()
                .Property(i => i.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Photo>()
                .HasOne(user => user.Owner)
                .WithMany(photos => photos.Photos);
        }
    }
}