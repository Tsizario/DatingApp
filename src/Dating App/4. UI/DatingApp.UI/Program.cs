using DatingApp.DAL.ApplicationSeed;

namespace DatingApp.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var startup = new Startup(builder.Configuration);

            startup.ConfigureServices(builder.Services);

            var app = builder.Build();

            startup.Configure(app);

            if (args.Length == 1 && args[0].ToLower() == "/seed")
            {
                RunSeeding(app);
            }
            else
            {
                app.Run();
            }
        }

        private static void RunSeeding(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                try
                {
                    var seeder = scope.ServiceProvider.GetRequiredService<AppUserSeed>();
                    seeder.SeedUsersAsync().Wait();
                    logger.LogInformation("The seeding is done");                    
                }
                catch(Exception ex)
                {
                    logger.LogError(ex, "An error occured duting seeding");
                }
            }
        }
    }
}
