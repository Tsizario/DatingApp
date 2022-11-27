using DatingApp.BLL.MapperProfile;
using DatingApp.BLL.Services.CloudinaryService;
using DatingApp.BLL.Services.ImageService;
using DatingApp.BLL.Services.PhotoService;
using DatingApp.BLL.Services.TokenService;
using DatingApp.BLL.Services.UserService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bll.Extensions
{
    public static class ServiceRegistrationExtensions
    {
        public static IServiceCollection AddBllServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPhotoService, PhotoService>();

            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));
            services.AddScoped<ICloudPhotoService, CloudPhotoService>();            

            return services;
        }
    }
}
