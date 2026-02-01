using OMS.Enums;

namespace OMS.Domain.Models
{
    public class DiscountCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DiscountType Type { get; set; }
        public decimal Value { get; set; }
        public int Priority { get; set; }
        public bool IsActive { get; set; }

        public DiscountCategory(string name, DiscountType type, decimal value, int priority, bool isActive = true)
        {
            Id = Guid.NewGuid();
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Type = type;
            Value = value;
            Priority = priority;
            IsActive = isActive;
        }

        public DiscountCategory() { }
    }
}
