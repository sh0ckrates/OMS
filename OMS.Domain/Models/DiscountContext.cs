using OMS.Models;

namespace OMS.Domain.Models
{
    public sealed class DiscountContext(Order order)
    {
        public Order Order { get; } = order ?? throw new ArgumentNullException(nameof(order));
        public decimal CurrentPrice { get; private set; } = order.InitialPrice;

        public void ApplyDiscount(decimal amount)
        {
            amount = Math.Round(amount, 2, MidpointRounding.ToEven);
            CurrentPrice = Math.Max(0m, Math.Round(CurrentPrice - amount, 2, MidpointRounding.ToEven));
        }
    }
}