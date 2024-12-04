using System.Linq.Expressions;

namespace Bulky.DataAccess.Repository.IRepository
{
	/// <summary>
	/// This is a generic interface to define the basic CRUD operations
	/// </summary>
	/// <typeparam name="T">Any class that need to implement this interface</typeparam>
	public interface IRepository<T> where T : class
	{
		// T - Generic Type class (Category, Product, ...etc)

		/// <summary>
		/// This is a generic method to get all records from the database
		/// </summary>
		/// <returns>List of records</returns>
		IEnumerable<T> GetAll();

		/// <summary>
		/// This is a generic method to get the first record that matches the filter
		/// </summary>
		/// <param name="filter">Filtering logic</param>
		/// <returns>Filtered record if exists, otherwise null</returns>
		T? GetFirstOrDefault(Expression<Func<T, bool>> filter);

		/// <summary>
		/// This is a generic method to add a new record to the database
		/// </summary>
		/// <param name="entity">The record to add to database</param>
		void Add(T entity);

		/// <summary>
		/// This is a generic method to remove a record from the database
		/// </summary>
		/// <param name="entity">The record to remove</param>
		void Remove(T entity);

		/// <summary>
		/// This is a generic method to remove a range of records from the database
		/// </summary>
		/// <param name="entity">Record list to remove</param>
		void RemoveRange(IEnumerable<T> entity);
	}
}
