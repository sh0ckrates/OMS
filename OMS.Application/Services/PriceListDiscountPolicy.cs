using Application.Services.Interfaces;
using Application.Services.Interfaces.Repo;
using OMS.Domain.Models;
using OMS.Enums;

namespace OMS.Application.Services
{
    public sealed class PriceListDiscountPolicy(IDiscountCategoryRepository discountCategoryRepo) : IDiscountPolicy
    {
        public string Name => "PriceList";
        public int Priority { get; set; }

        public async Task<bool> IsEligibleAsync(decimal currentPrice, CancellationToken ct = default)
        {
            var cat = await discountCategoryRepo.GetActiveDiscountCategoryByName(Name, ct);
            return cat is not null && currentPrice > 0m;
        }

        public async Task<DiscountResult> GetDiscountAsync(decimal currentPrice, CancellationToken ct = default)
        {
            var cat = await discountCategoryRepo.GetActiveDiscountCategoryByName(Name, ct);
            if (cat is null) return null;
            Priority = cat.Priority;

            decimal amount = cat.Type == DiscountType.Percentage ? currentPrice * cat.Value : cat.Value;

            if (amount <= 0m) return null;

            return new DiscountResult(cat.Name, cat.Type, amount, currentPrice - amount);
        }
    }
}