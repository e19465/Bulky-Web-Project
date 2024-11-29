using Bulky_Web.Data;
using Microsoft.EntityFrameworkCore;

namespace Bulky_Web.Extensions
{
    public static class EntityFrameworkCoreExtensions
    {
        public static IServiceCollection InjectDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            return services;

        }
    }
}
