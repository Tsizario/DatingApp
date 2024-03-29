﻿using AutoMapper;
using DatingApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.DAL.Repositories.UserRepository
{
    internal class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserRepository(ApplicationDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<AppUser> GetByIdAsync(int userId)
        {
            return await _dbContext.Users
                .FindAsync(userId);
        }

        public async Task<AppUser> GetByUsernameAsync(string username)
        {
            return await _dbContext.Users
                .Include(x => x.Photos)
                .SingleOrDefaultAsync(user => user.Username == username);
        }

        public async Task<IEnumerable<AppUser>> GetAllUsersAsync()
        {
            return await _dbContext.Users
                .Include(x => x.Photos)
                .ToListAsync();
        }

        public async Task<AppUser> AddAsync(AppUser appUser)
        {
            await _dbContext.Users.AddAsync(appUser);

            await _dbContext.SaveChangesAsync();

            return appUser;
        }

        public async Task<AppUser> UpdateAsync(AppUser updatedUser)
        {
            var user = await GetByIdAsync(updatedUser.Id);

            if (user == null)
            {
                return null;
            }

            _mapper.Map(updatedUser, user);           
            _dbContext.Entry(user).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<bool> ExistsAsync(string username)
        {
            return await _dbContext.Users
                .AnyAsync(user => user.Username == username.ToLower());
        }       
    }
}
