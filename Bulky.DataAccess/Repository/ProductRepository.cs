using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;

namespace Bulky.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// This method responsible for saving Product entity to database
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
        /// This is responsible for updating Product in DB
        /// </summary>
        /// <param name="product"></param>
        public void Update(Product product)
        {
            try
            {
                _dbContext.Products.Update(product);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
