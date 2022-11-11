using DatingApp.BLL.MapperProfile;
using DatingApp.BLL.Services.TokenService;
using DatingApp.BLL.Services.UserService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bll.Extensions
{
    public static class ServiceRegistrationExtensions
    {
        public static IServiceCollection AddBusinessLogicLayerServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            return services;
        }
    }
}
