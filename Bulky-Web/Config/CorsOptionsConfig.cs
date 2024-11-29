using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Bulky_Web.Config
{
    public static class CorsOptionsConfig
    {
        public static void ConfigureCorsPolicy(CorsPolicyBuilder options)
        {
            // Define CORS options
            var allowedOrigins = new[] { "https://localhost:7065" };
            var allowedHeaders = new[] {
                "accept",
                "authorization",
                "content-type",
                "user-agent",
                "x-csrftoken",
                "x-requested-with"
            };
            var allowedMethods = new[] {
                "GET",
                "POST",
                "PUT",
                "PATCH",
                "DELETE",
                "OPTIONS"
            };
            var allowCredentials = true;


            // Configure CORS policy
            options.WithOrigins(allowedOrigins)
                .WithHeaders(allowedHeaders)
                .WithMethods(allowedMethods);

            if (allowCredentials)
            {
                options.AllowCredentials();
            }
        }
    }

}
