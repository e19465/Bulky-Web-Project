using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;

namespace Bulky_Web.Extensions
{
	public static class DependencyInjectionExtensions
	{
		public static IServiceCollection InjectServiceScopes(this IServiceCollection services)
		{
			services.AddScoped<ICategoryRepository, CategoryRepository>();
			return services;

		}
	}
}
