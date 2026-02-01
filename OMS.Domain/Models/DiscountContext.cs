// OMS.Domain/Models/DiscountContext.cs
using OMS.Models;

namespace OMS.Domain.Models
{
    public sealed class DiscountContext(Order order)
    {
        public Order Order { get; } = order ?? throw new ArgumentNullException(nameof(order));
        public decimal CurrentPrice { get; private set; } = order.InitialPrice;

        public void ApplyDiscount(decimal amount)
        {
            amount = Round2(amount);
            CurrentPrice = Math.Max(0m, Round2(CurrentPrice - amount));
        }

        private static decimal Round2(decimal v) => Math.Round(v, 2, MidpointRounding.ToEven);
    }
}