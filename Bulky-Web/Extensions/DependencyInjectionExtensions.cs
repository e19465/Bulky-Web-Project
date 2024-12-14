using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Bulky_Web.helpers;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Bulky_Web.Extensions
{
	public static class DependencyInjectionExtensions
	{
		public static IServiceCollection InjectServiceScopes(this IServiceCollection services)
		{
			services.AddScoped<ICategoryRepository, CategoryRepository>();
			services.AddScoped<IProductRepository, ProductRepository>();
			services.AddScoped<FileUploadService>();
			services.AddScoped<ArrangeQueryIncludeTypes>();
			services.AddScoped<IEmailSender, EmailSender>();
            return services;

		}
	}
}
