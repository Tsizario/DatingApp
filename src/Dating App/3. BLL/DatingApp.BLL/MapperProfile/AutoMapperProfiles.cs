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
            CreateMap<AppUser, MemberDto>()
                .ForMember(destination => destination.Age, optional =>
                    optional.MapFrom(source => source.DateOfBirth.CalculateAge()));
            CreateMap<AppUser, AppUserRegisterDto>().ReverseMap();
            CreateMap<AppUser, AppUserLoginDto>();
            CreateMap<AppUser, AppUserTokenDto>();

            CreateMap<Photo, PhotoDto>();
        }
    }
}
