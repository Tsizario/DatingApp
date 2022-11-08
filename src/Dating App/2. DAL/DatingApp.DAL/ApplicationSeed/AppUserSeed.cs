using DatingApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace DatingApp.DAL.ApplicationSeed
{
    public class AppUserSeed
    {
        private readonly AppContext _context;

        public AppUserSeed(AppContext context)
        {
            _context = context;
        }

        public async Task SeedUsersAsync()
        {
            _context.Database.EnsureCreated();

            if (await _context.Users.AnyAsync())
                return;

            var filePath = new DirectoryInfo(
                    Path.Combine(Environment.CurrentDirectory,
                        @"..\..\2. DAL\DAL\ApplicationSeed"))
                            .GetFiles("*.json")
                                .FirstOrDefault()?.FullName;

            var userData = await File.ReadAllTextAsync(filePath);
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);

            foreach (var user in users)
            {
                using var hmac = new HMACSHA256();

                user.Username = user.Username.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("password"));
                user.PasswordSalt = hmac.Key;

                _context.Users.Add(user);
            }

            await _context.SaveChangesAsync();
        }
    }
}
