using BLL.Helpers;
using DatingApp.BLL.DTO;
using DatingApp.BLL.DTO.Photo;
using DatingApp.Domain.Entities;

namespace DatingApp.BLL.Services.PhotoService
{
    public interface IPhotoService
    {
        Task<ServiceResult<PhotoReadDto>> AddPhoto(PhotoDto photoDto);

        Task<ServiceResult<bool>> DeletePhoto(int photoId);
    }
}
