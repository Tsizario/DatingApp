using AutoMapper;
using DatingApp.BLL.DTO;
using DatingApp.Domain.Entities;

namespace DatingApp.BLL.MapperProfile
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, AppUserDto>()
                .ForMember(destination => destination.PhotoUrl, optional => optional.MapFrom(source =>
                    source.Photos.FirstOrDefault(photo => photo.IsMain).Url));
        }
    }
}
