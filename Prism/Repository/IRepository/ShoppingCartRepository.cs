using Microsoft.EntityFrameworkCore;
using Prism.Data;

namespace Prism.Repository.IRepository;
public class ShoppingCartRepository : IShoppingCartRepository
{
    private readonly ApplicationDbContext _db;
    public ShoppingCartRepository(ApplicationDbContext db )
    {
        _db = db;
    }
    public async Task<IEnumerable<ShoppingCart>> GetAllCartsAsync(string? userId)
    {
        return await _db.ShoppingCart.Where(u => u.UserId == userId).Include(p => p.Product).ToListAsync();
    }

    public async Task<bool> UpdateCartAsync(string userId, int productId, int updateBy)
    {
        if(string.IsNullOrEmpty(userId))
        {
            return false;
        }
        ShoppingCart? cart = await _db.ShoppingCart.FirstOrDefaultAsync(u => u.UserId == userId && u.ProductId == productId);

        if (cart != null)
        {
            cart.Count += updateBy;
            if (cart.Count <= 0)
            {
                _db.ShoppingCart.Remove(cart);
            }
            else
            {
                _db.ShoppingCart.Update(cart);
            }
        }
        else
        {
            if (updateBy > 0)
            {
                ShoppingCart newCart = new ShoppingCart()
                {
                    UserId = userId,
                    ProductId = productId,
                    Count = updateBy
                };
                await _db.ShoppingCart.AddAsync(newCart);
            }
            else
            {
                return false;
            }
        }
        return await _db.SaveChangesAsync() > 0;
    }

    public async Task<bool> ClearCartAsync(string? userId)
    {
        if(userId != null)
        {
            var cartItems = _db.ShoppingCart.Where(u => u.UserId == userId);
            _db.ShoppingCart.RemoveRange(cartItems);
            return await _db.SaveChangesAsync() > 0;
        }
        else
        {
            return false;
        }
    }

    public async Task<int> GetTotalCartCountAsync(string? userId)
    {
        int cartCount = 0;
        var cartItems = await _db.ShoppingCart.Where(u => u.UserId == userId).ToListAsync();

        foreach(var item in cartItems)
        {
            cartCount += item.Count;
        }

        return cartCount;
    }
}