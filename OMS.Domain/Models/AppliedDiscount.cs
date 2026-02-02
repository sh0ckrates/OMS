namespace OMS.Domain.Models
{
    public class AppliedDiscount
    {
        public Guid Id { get; set; }
        public DiscountCategory Category { get; set; }
        public string DiscountName { get; set; }
        public decimal Amount { get; set; }
        public decimal PriceAfter { get; set; }
        public DateTime AppliedAt { get; set; } = DateTime.UtcNow;

        public AppliedDiscount(DiscountCategory category, string discountName, decimal amount, decimal priceAfter, Guid id = default)
        {
            Id = id != Guid.Empty ? id : Guid.NewGuid();
            Category = category ?? throw new ArgumentNullException(nameof(category));
            DiscountName = discountName;
            Amount = amount;
            PriceAfter = priceAfter;
        }

        public AppliedDiscount() { }
    }
}
