using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;

namespace Bulky.DataAccess.Repository
{
	public class CategoryRepository : Repository<Category>, ICategoryRepository
	{
		private readonly ApplicationDbContext _dbContext;
		public CategoryRepository(ApplicationDbContext db) : base(db)
		{
            _dbContext = db;
		}

		/// <summary>
		/// This method responsive for saving category entity to database
		/// </summary>
        public void Save()
		{
			try
			{
				_dbContext.SaveChanges();
			}
			catch (Exception)
			{
				throw;
			}
		}


		/// <summary>
		/// This method responsible for updating the category entity
		/// </summary>
		/// <param name="category">Category to be updated with new values</param>
		public void Update(Category category)
		{
			try
			{
				_dbContext.Categories.Update(category);
            }
			catch (Exception)
			{
				throw;
			}
		}
	}
}
