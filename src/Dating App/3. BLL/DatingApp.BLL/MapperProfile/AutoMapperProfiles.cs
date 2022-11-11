using AutoMapper;
using DatingApp.BLL.DTO;
using DatingApp.Domain.Entities;

namespace DatingApp.BLL.MapperProfile
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, AppUserDto>();
            CreateMap<AppUser, AppUserRegisterDto>().ReverseMap();
        }
    }
}
