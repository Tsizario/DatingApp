using AutoMapper;
using DatingApp.BLL.DTO.Photo;
using DatingApp.Domain.Entities;

namespace DatingApp.BLL.MapperProfile
{
    public class PhotoProfile : Profile
    {
        public PhotoProfile()
        {
            CreateMap<Photo, PhotoDto>().ReverseMap();

            CreateMap<Photo, PhotoReadDto>();
        }
    }
}
