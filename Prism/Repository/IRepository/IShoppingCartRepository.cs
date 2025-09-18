using Prism.Data;

namespace Prism.Repository.IRepository
{
    public interface IShoppingCartRepository
    {
        public Task<bool> UpdateCartAsync(string userId, int productId, int updateBy);
        public Task<IEnumerable<ShoppingCart>> GetAllCartsAsync(string? userId);
        public Task<bool> ClearCartAsync(string? userId);
    }
}
