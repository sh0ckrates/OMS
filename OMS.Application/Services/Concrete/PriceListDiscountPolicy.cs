using Application.Services.Interfaces;
using Application.Services.Interfaces.Repo;
using OMS.Domain.Models;
using OMS.Enums;

namespace Application.Services.Concrete
{
    public sealed class PriceListDiscountPolicy(IDiscountCategoryRepository repo) : IDiscountPolicy
    {
        public string Name => "PriceList";
        public int Priority => 1;

        public async Task<bool> IsEligibleAsync(DiscountContext context, CancellationToken ct = default)
        {
            var cat = await repo.GetActiveByNameAsync(Name, ct);
            return cat is not null && context.CurrentPrice > 0m;
        }

        public async Task<DiscountResult> GetDiscountAsync(DiscountContext context, CancellationToken ct = default)
        {
            var cat = await repo.GetActiveByNameAsync(Name, ct);
            if (cat is null) return null;

            decimal amount = cat.Type == DiscountType.Percentage
                ? context.CurrentPrice * cat.Value
                : cat.Value;

            if (amount <= 0m) return null;

            return new DiscountResult(cat.Name, cat.Type, amount, context.CurrentPrice);
        }
    }
}