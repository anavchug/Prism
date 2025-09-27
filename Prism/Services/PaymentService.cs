using Microsoft.AspNetCore.Components;
using Prism.Data;
using Prism.Repository.IRepository;
using Stripe;
using Stripe.Checkout;
using Stripe.Climate;

namespace Prism.Services
{
    public class PaymentService
    {
        private readonly NavigationManager _navigationManager = null!;
        private readonly IOrderRepository _orderRepository = null!;

        public PaymentService(NavigationManager navigationManager, IOrderRepository orderRepository)
        {
            _navigationManager = navigationManager;
            _orderRepository = orderRepository;
        }

        public Session CreateStripeCheckoutSession(OrderHeader orderHeader)
        {
            var lineItems = orderHeader.OrderDetails.Select(item => new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long)(item.Price * 100),
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = item.ProductName,
                    },
                },
                Quantity = item.Count,
            }).ToList();

            var options = new SessionCreateOptions
            { 
                SuccessUrl = $"{_navigationManager.BaseUri}order/confirmation/{{CHECKOUT_SESSION_ID}}",
                CancelUrl = $"{_navigationManager.BaseUri}cart",
                LineItems = lineItems,
                Mode = "payment",
            };

            var service = new SessionService();
            Session session = service.Create(options);

            return session;
        }

        public async Task<OrderHeader?> CheckPaymentStatusAndUpdateOrder(string sessionId)
        {
            var service = new SessionService();
            var session = service.Get(sessionId);

            if (session.PaymentStatus.ToLower() == "paid")
            {
                var orderHeader = await _orderRepository.GetOrderBySessionIdAsync(sessionId);

                if (orderHeader != null)
                {
                    orderHeader.Status = "Approved";
                    orderHeader.PaymentIntentId = session.PaymentIntentId;
                    await _orderRepository.UpdateOrderStatus(orderHeader.Id, orderHeader.Status, orderHeader.PaymentIntentId);
                    return orderHeader;
                }
            }

            return null;
        }
    }
}
