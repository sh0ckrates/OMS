using OMS.Enums;

namespace OMS.Infrastructure.Entities
{
    public class DiscountCategoryEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;   // "PriceList", "Promotion", "Coupon"
        public DiscountType Type { get; set; }             // Percentage | Fixed
        public decimal Value { get; set; }                 // e.g., 0.05 (5%), 0.10 (10%), 10.00
        public int Priority { get; set; }                  // 1,2,3...
        public bool IsActive { get; set; } = true;

        public ICollection<AppliedDiscountEntity> AppliedDiscounts { get; set; } = new List<AppliedDiscountEntity>();
    }
}