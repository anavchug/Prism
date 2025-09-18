using Prism.Data;

namespace Prism.Utility
{
    public static class OrderStatus
    {
        public static string StatusPending = "Pending";
        public static string StatusReadyForPickup = "ReadyForPickup";
        public static string StatusCompleted = "Completed";
        public static string StatusCancelled = "Cancelled";

        public static List<OrderDetail> ConvertShoppingCartListToOrderDetail(List<ShoppingCart> shoppingCarts)
        {
            var orderDetails = new List<OrderDetail>();
            foreach (var cart in shoppingCarts)
            {
                var orderDetail = new OrderDetail
                {
                    ProductId = cart.ProductId,
                    Count = cart.Count,
                    Price = (double) cart.Product.Price,
                    ProductName = cart.Product.Name,
                };
                orderDetails.Add(orderDetail);
            }
            return orderDetails;
        }
    }
}
