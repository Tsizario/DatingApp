using AutoMapper;
using BLL.Helpers;
using DatingApp.BLL.DTO;
using DatingApp.DAL.Repositories.PhotoRepository;
using DatingApp.Domain.Constants;
using DatingApp.Domain.Entities;

namespace DatingApp.BLL.Services.PhotoService
{
    public class PhotoService : IPhotoService
    {
        private readonly IPhotoRepository _photoRepository;
        private readonly IMapper _mapper;

        public PhotoService(IPhotoRepository photoRepository,
            IMapper mapper)
        {
            _photoRepository = photoRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<PhotoReadDto>> AddPhoto(PhotoDto photoDto)
        {
            var photo = _mapper.Map<Photo>(photoDto);

            photo = await _photoRepository.AddAsync(photo);

            var photoReadDto = _mapper.Map<PhotoReadDto>(photo);

            return photoReadDto is not null
                ? ServiceResult<PhotoReadDto>.CreateSuccess(photoReadDto)
                : ServiceResult<PhotoReadDto>.CreateFailure(Errors.PhotoAddingError);
        }

        public async Task<ServiceResult<bool>> DeletePhoto(int photoId)
        {
            var result = await _photoRepository.DeleteAsync(photoId);

            return result
                ? ServiceResult<bool>.CreateSuccess(result)
                : ServiceResult<bool>.CreateFailure(Errors.PhotoDoesNotExists);
        }
    }
}
