using Microsoft.EntityFrameworkCore;
using Prism.Data;

namespace Prism.Repository.IRepository
{
    public interface IProductRepository
    {
        public Task<Product> CreateProductAsync(Product category);
        public Task<Product> UpdateProductAsync(Product category);
        public Task<bool> DeleteProductAsync(int Id);
        public Task<Product> GetProductAsync(int Id);
        public Task<IEnumerable<Product>> GetAllProductAsync();

       



    }
}
