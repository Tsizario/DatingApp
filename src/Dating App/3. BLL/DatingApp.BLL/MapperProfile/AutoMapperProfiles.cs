using AutoMapper;
using DatingApp.BLL.DTO;
using DatingApp.BLL.Extensions;
using DatingApp.Domain.Entities;

namespace DatingApp.BLL.MapperProfile
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, AppUserDto>()
                .ForMember(destination => destination.Age, optional =>
                    optional.MapFrom(source => source.DateOfBirth.CalculateAge()))
                .ForMember(destination => destination.PhotoUrl, optional =>
                    optional.MapFrom(source => source.Photos
                        .SingleOrDefault(x => x.IsMain)!.Url));

            CreateMap<AppUser, AppUserRegisterDto>().ReverseMap();

            CreateMap<AppUser, AppUserLoginDto>();

            CreateMap<AppUser, AppUserTokenDto>();

            CreateMap<Photo, PhotoDto>().ReverseMap();

            CreateMap<Photo, PhotoReadDto>();
        }
    }
}
