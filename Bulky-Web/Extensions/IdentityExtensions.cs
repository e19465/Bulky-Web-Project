using Bulky.DataAccess.Data;
using Microsoft.AspNetCore.Identity;

namespace Bulky_Web.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection InjectIdentityHandlersAndStores(this IServiceCollection services)
        {
            services.AddDefaultIdentity<IdentityUser>(options =>
                        options.SignIn.RequireConfirmedAccount = true)
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();
            return services;
        }
    }
}
