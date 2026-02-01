using Application.Services.Interfaces;
using Application.Services.Interfaces.Repo;
using OMS.Domain.Models;
using OMS.Enums;

namespace Application.Services.Concrete
{

    namespace OMS.Domain.Policies
    {
        public sealed class CouponDiscountPolicy(IDiscountCategoryRepository repo) : IDiscountPolicy
        {
            public string Name => "Coupon";
            public int Priority => 3;

            public async Task<bool> IsEligibleAsync(DiscountContext context, CancellationToken ct = default)
                => (await repo.GetActiveByNameAsync(Name, ct)) is not null && context.CurrentPrice > 0m;

            public async Task<DiscountResult?> GetDiscountAsync(DiscountContext context, CancellationToken ct = default)
            {
                var cat = await repo.GetActiveByNameAsync(Name, ct);
                if (cat is null) return null;

                var amount = cat.Type == DiscountType.Percentage ? context.CurrentPrice * cat.Value : cat.Value;
                return amount <= 0m ? null : new DiscountResult(cat.Name, cat.Type, amount, context.CurrentPrice);
            }
        }
    }

}
