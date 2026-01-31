using OMS.Models.OMS.Domain.Models;

namespace OMS.Models
{
    /// <summary>
    /// Represents the result of applying a discount.
    /// </summary>
    public class DiscountResult(DiscountCategory category, decimal amount, decimal priceAfter)
    {
        public DiscountCategory Category { get; } = category;
        public decimal Amount { get; } = amount;
        public decimal PriceAfter { get; } = priceAfter;
    }

}
