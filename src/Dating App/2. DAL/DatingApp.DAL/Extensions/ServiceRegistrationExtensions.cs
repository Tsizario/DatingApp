using DatingApp.DAL.ApplicationSeed;
using DatingApp.DAL.Repositories.PhotoRepository;
using DatingApp.DAL.Repositories.UserRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DatingApp.DAL.Extensions
{
    public static class ServiceRegistrationExtensions
    {
        public static IServiceCollection AddDalServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(config =>
            {
                config.UseSqlServer(
                    configuration.GetConnectionString("CourseDatingAppConnection"));
            });

            services.AddScoped<AppUserSeed>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPhotoRepository, PhotoRepository>();

            return services;
        }
    }
}
