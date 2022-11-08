using DatingApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.DAL
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions options)
        : base(options)
        {
        }

        public DbSet<AppUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AppUser>()
                .Property(i => i.Id)
                .ValueGeneratedOnAdd();
        }
    }
}