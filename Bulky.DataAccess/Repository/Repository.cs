using System.Linq.Expressions;
using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Bulky.DataAccess.Repository
{
	/// <summary>
	/// This is a generic class to define the basic CRUD operations
	/// </summary>
	/// <typeparam name="T">The class T</typeparam>
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly DbSet<T> _dbSet;

		public Repository(ApplicationDbContext dbContext)
        {
			_dbContext = dbContext;
			_dbSet = dbContext.Set<T>();
		}

		/// <summary>
		/// This is a generic method to add a new record to the database
		/// </summary>
		/// <param name="entity">The entity to add</param>
		public void Add(T entity)
		{
			try
			{
				_dbSet.Add(entity);
			}catch (Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// This is a generic method to get all records from the database
		/// </summary>
		/// <returns>List of antities in corresponding type</returns>
		public IEnumerable<T> GetAll()
		{
			try
			{
				IQueryable<T> query = _dbSet;
				return query.ToList();
			}
			catch (Exception)
			{
				throw;
			}

		}


		/// <summary>
		/// This is a generic method to get the first record that matches the filter
		/// </summary>
		/// <param name="filter">The logic to filter</param>
		/// <returns>Filtered entity if exists, otherwise null</returns>
		public T? GetFirstOrDefault(Expression<Func<T, bool>> filter)
		{
			try
			{
				IQueryable<T> query = _dbSet;
				query = query.Where(filter);
				return query.FirstOrDefault();
			}catch(Exception)
			{
				throw;
			}
		}


		/// <summary>
		/// This is a generic method to remove a record from the database
		/// </summary>
		/// <param name="entity">The entity to remove</param>
		public void Remove(T entity)
		{
			try
			{
				_dbSet.Remove(entity);
			}
			catch (Exception)
			{
				throw;
			}
		}


		/// <summary>
		/// This is a generic method to remove a range of records from the database
		/// </summary>
		/// <param name="entities">The entities list to remove</param>
		public void RemoveRange(IEnumerable<T> entities)
		{
			try
			{
				_dbSet.RemoveRange(entities);
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
