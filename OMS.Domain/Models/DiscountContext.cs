namespace OMS.Models
{
    /// <summary>
    /// Tracks current price as discounts are applied.
    /// </summary>
    public class DiscountContext(Order order)
    {
        public Order Order { get; } = order;
        public decimal CurrentPrice { get; private set; } = order.InitialPrice;

        public void ApplyDiscount(decimal amount)
        {
            CurrentPrice -= amount;
            if (CurrentPrice < 0)
                CurrentPrice = 0;
        }
    }
}

