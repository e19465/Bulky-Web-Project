using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Bulky_Web.helpers;

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
            return services;

		}
	}
}
