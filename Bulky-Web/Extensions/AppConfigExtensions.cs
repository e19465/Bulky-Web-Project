using Bulky_Web.Config;

namespace Bulky_Web.Extensions
{
    public static class AppConfigExtensions
    {
        public static WebApplication ConfigureCORS(this WebApplication app, IConfiguration configuration)
        {
            app.UseCors(options => CorsOptionsConfig.ConfigureCorsPolicy(options));
            return app;
        }
    }
}
