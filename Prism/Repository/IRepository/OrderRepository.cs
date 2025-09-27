using Microsoft.EntityFrameworkCore;
using Prism.Data;

namespace Prism.Repository.IRepository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<OrderHeader> CreateOrderAsync(OrderHeader orderHeader)
        {
            orderHeader.OrderDate = DateTime.Now;
            await _db.OrderHeader.AddAsync(orderHeader);
            await _db.SaveChangesAsync();
            return orderHeader;
        }

        public async Task<IEnumerable<OrderHeader>> GetAllOrdersAsync(string? userId = null)
        {
            if(!string.IsNullOrEmpty(userId))
            {
                return await _db.OrderHeader.Where(o => o.UserId == userId).ToListAsync();
            }
            return await _db.OrderHeader.ToListAsync();
        }

        public async Task<OrderHeader?> GetOrderAsync(int orderId)
        {
            return await _db.OrderHeader
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<OrderHeader?> GetOrderBySessionIdAsync(string sessionId)
        {
            return await _db.OrderHeader.FirstOrDefaultAsync(o => o.SessionId == sessionId);
        }

        public async Task<OrderHeader?> UpdateOrderStatus(int orderId, string status, string paymentIntentId)
        {
           var orderHeader = await _db.OrderHeader.FindAsync(orderId);
            if (orderHeader != null)
            {
                orderHeader.Status = status;

                if(!string.IsNullOrEmpty(paymentIntentId))
                {
                    orderHeader.PaymentIntentId = paymentIntentId;
                }

                await _db.SaveChangesAsync();
                return orderHeader;
            }

            return null;
        }
            
    }
}
