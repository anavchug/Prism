using Microsoft.EntityFrameworkCore;
using Prism.Data;

namespace Prism.Repository.IRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private IWebHostEnvironment _webHostEnvironment;
        public ProductRepository(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<Product> CreateProductAsync(Product product)
        {
            await _db.Product.AddAsync(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            var existingProduct = await _db.Product.FirstOrDefaultAsync(c => c.Id == product.Id);

            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Description = product.Description;
                existingProduct.imageUrl = product.imageUrl;
                existingProduct.CategoryId = product.CategoryId;

                _db.Update(existingProduct);
                await _db.SaveChangesAsync();
                return existingProduct;
            }

            return product;
        }

        public async Task<bool> DeleteProductAsync(int Id)
        {
            var product = await _db.Product.FirstOrDefaultAsync(c => c.Id == Id);

            if (product != null)
            {
                if (!string.IsNullOrEmpty(product.imageUrl))
                {
                    var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, product.imageUrl.TrimStart('/'));
                    if(File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                }
                _db.Product.Remove(product);
                return await _db.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<Product> GetProductAsync(int Id)
        {
            var product = await _db.Product.FirstOrDefaultAsync(c => c.Id == Id);

            if (product != null)
            {
                return product;
            }
            return new Product();
        }

        public async Task<IEnumerable<Product>> GetAllProductAsync()
        {
            return await _db.Product.Include(c => c.Category).ToListAsync();
        }
    }
}
