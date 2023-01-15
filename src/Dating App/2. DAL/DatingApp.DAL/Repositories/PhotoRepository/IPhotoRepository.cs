using DatingApp.Domain.Entities;

namespace DatingApp.DAL.Repositories.PhotoRepository
{
    public interface IPhotoRepository
    {
        Task<Photo> AddAsync(Photo photo);

        Task<bool> DeleteAsync(int id);
    }
}
