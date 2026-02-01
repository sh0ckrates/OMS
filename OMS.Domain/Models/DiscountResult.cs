// OMS.Domain/Models/DiscountResult.cs
using OMS.Enums;

namespace OMS.Domain.Models
{
    public sealed class DiscountResult(string name, DiscountType kind, decimal amountApplied, decimal priceAfter)
    {
        public string Name { get; } = name ?? throw new ArgumentNullException(nameof(name));
        public DiscountType Kind { get; } = kind;
        public decimal AmountApplied { get; } = amountApplied;
        public decimal PriceAfter { get; } = priceAfter;
    }
}