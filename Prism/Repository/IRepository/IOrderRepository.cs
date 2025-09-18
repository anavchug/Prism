using Prism.Data;

namespace Prism.Repository.IRepository
{
    public interface IOrderRepository
    {
        public Task<OrderHeader> CreateOrderAsync(OrderHeader orderHeader);
        public Task<OrderHeader?> GetOrderAsync(int orderId);
        public Task<IEnumerable<OrderHeader>> GetAllOrdersAsync(string? userId = null);
        public Task<OrderHeader?> UpdateOrderStatus(int orderId, string status);
    }
}
