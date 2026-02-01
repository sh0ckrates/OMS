using OMS.Enums;

namespace OMS.Infrastructure.Entities
{
    public class DiscountCategoryEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DiscountType Type { get; set; }
        public decimal Value { get; set; }
        public int Priority { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<AppliedDiscountEntity> AppliedDiscounts { get; set; } = [];
    }
}