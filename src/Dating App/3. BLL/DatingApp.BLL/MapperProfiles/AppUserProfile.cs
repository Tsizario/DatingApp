using AutoMapper;
using DatingApp.BLL.DTO.AppUser;
using DatingApp.BLL.Extensions;
using DatingApp.Domain.Entities;

namespace DatingApp.BLL.MapperProfile
{
    public class AppUserProfile : Profile
    {
        private static Mapper _mapper { get; set; }

        public AppUserProfile()
        {
            CreateMap<AppUser, AppUserDto>()
                .ForMember(destination => destination.Age, optional =>
                    optional.MapFrom(source => source.DateOfBirth.CalculateAge()))
                .ForMember(destination => destination.PhotoUrl, optional =>
                    optional.MapFrom(source => source.Photos
                        .SingleOrDefault(x => x.IsMain)!.Url)).ReverseMap();

            CreateMap<AppUserDto, AppUser>()
                .ForMember(destination => destination.Photos, optional =>
                    optional.Ignore())
                        .AfterMap((appUserDto, appUser) => AddOrUpdate(appUserDto, appUser));

            CreateMap<AppUser, AppUserRegisterDto>().ReverseMap();

            CreateMap<AppUser, AppUserLoginDto>();

            CreateMap<AppUser, AppUserTokenDto>();
        }

        private void AddOrUpdate(AppUserDto dto, AppUser user)
        {
            foreach (var photoDto in dto.Photos)
            {
                if (photoDto.Id == default(int))
                {
                    user.Photos.Add(_mapper.Map<Photo>(photoDto));
                }
                else
                {
                    _mapper.Map(photoDto, user.Photos.SingleOrDefault(c => c.Id == photoDto.Id));
                }
            }
        }
    }
}
