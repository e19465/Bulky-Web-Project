using Bulky.Models;

namespace Bulky.DataAccess.Repository.IRepository
{
	/// <summary>
	/// This interface is responsible for defining the basic CRUD operations for Category entity
	/// This interface inherits from IRepository interface
	/// IRepository interface is a generic interface that defines the basic CRUD operations for all entities
	/// </summary>
	public interface ICategoryRepository : IRepository<Category>
	{
		/// <summary>
		/// This method responsible for updating the category entity
		/// </summary>
		/// <param name="category">The category with updated values</param>
		void Update(Category category);

		/// <summary>
		/// This method responsible for saving category entity to database
		/// </summary>
		void Save();
	}
}
