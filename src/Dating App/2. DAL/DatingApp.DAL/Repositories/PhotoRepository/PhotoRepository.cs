using DatingApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.DAL.Repositories.PhotoRepository
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PhotoRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Photo> AddAsync(Photo photo)
        {
            await _dbContext.Photos.AddAsync(photo);

            await _dbContext.SaveChangesAsync();

            return photo;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var photo = await _dbContext.Photos.FindAsync(id);

            if (photo == null)
            {
                return false;
            }

            _dbContext.Photos.Remove(photo);

            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
