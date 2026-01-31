using OMS.Models;
using OMS.Models.OMS.Domain.Models;

namespace OMS.Domain.Models
{
    public class AppliedDiscount
    {
        public Guid Id { get; set; }
        public DiscountCategory Category { get; set; }  // Domain model
        public string DiscountName { get; set; }        // e.g., "Black Friday Promo"
        public decimal Amount { get; set; }             // Amount discounted
        public decimal PriceAfter { get; set; }         // Price after applying this discount
        public DateTime AppliedAt { get; set; } = DateTime.UtcNow;

        // Constructor
        public AppliedDiscount(Guid id, DiscountCategory category, string discountName, decimal amount, decimal priceAfter)
        {
            Id = id != Guid.Empty ? id : Guid.NewGuid();
            Category = category ?? throw new ArgumentNullException(nameof(category));
            DiscountName = discountName;
            Amount = amount;
            PriceAfter = priceAfter;
        }

        // Optional constructor without Id (new discount)
        public AppliedDiscount(DiscountCategory category, string discountName, decimal amount, decimal priceAfter)
        {
            Id = Guid.NewGuid();
            Category = category ?? throw new ArgumentNullException(nameof(category));
            DiscountName = discountName;
            Amount = amount;
            PriceAfter = priceAfter;
        }

        // Parameterless constructor for serialization if needed
        public AppliedDiscount() { }
    }
}
