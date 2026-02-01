using Application.Services.Interfaces;
using Application.Services.Interfaces.Repo;
using OMS.Domain.Models;
using OMS.Enums;

namespace OMS.Application.Services
{
    public sealed class CouponDiscountPolicy(IDiscountCategoryRepository repo) : IDiscountPolicy
    {
        public string Name => "Coupon";
        public int Priority { get; set; }

        public async Task<bool> IsEligibleAsync(DiscountContext context, CancellationToken ct = default)
            => (await repo.GetActiveDiscountCategoryByName(Name, ct)) is not null && context.CurrentPrice > 0m;

        public async Task<DiscountResult> GetDiscountAsync(DiscountContext context, CancellationToken ct = default)
        {
            var cat = await repo.GetActiveDiscountCategoryByName(Name, ct);
            if (cat is null) return null;
            Priority = cat.Priority;

            var amount = cat.Type == DiscountType.Percentage ? context.CurrentPrice * cat.Value : cat.Value;
            return amount <= 0m ? null : new DiscountResult(cat.Name, cat.Type, amount, context.CurrentPrice);
        }
    }
}
