using OMS.Domain.Models;

namespace OMS.Models
{
    public class DiscountSummary(decimal originalPrice, decimal finalPrice, IReadOnlyCollection<DiscountResult> discounts)
    {
        public decimal OriginalPrice { get; } = originalPrice;
        public decimal FinalPrice { get; } = finalPrice;
        public IReadOnlyCollection<DiscountResult> Discounts { get; } = discounts;
    }
}
