using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bulky.Models;

namespace Bulky.DataAccess.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        /// <summary>
        /// This is responsible for updating Product in DB
        /// </summary>
        /// <param name="product">Product to be updated</param>
        void Update(Product product);

        /// <summary>
        /// This method responsible for saving Product entity to database
        /// </summary>
        void Save();
    }
}
