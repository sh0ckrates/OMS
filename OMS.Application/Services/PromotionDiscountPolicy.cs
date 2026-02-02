using Application.Services.Interfaces;
using Application.Services.Interfaces.Repo;
using OMS.Domain.Models;
using OMS.Enums;

namespace OMS.Application.Services;

public sealed class PromotionDiscountPolicy(IDiscountCategoryRepository repo) : IDiscountPolicy
{
    public string Name => "Promotion";
    public int Priority { get; set; }

    public async Task<bool> IsEligibleAsync(decimal currentPrice, CancellationToken ct = default)
        => (await repo.GetActiveDiscountCategoryByName(Name, ct)) is not null && currentPrice > 0m;

    public async Task<DiscountResult> GetDiscountAsync(decimal currentPrice, CancellationToken ct = default)
    {
        var cat = await repo.GetActiveDiscountCategoryByName(Name, ct);
        if (cat is null) return null;
        Priority = cat.Priority;

        var amount = cat.Type == DiscountType.Percentage ? currentPrice * cat.Value : cat.Value;
        return amount <= 0m ? null : new DiscountResult(cat.Name, cat.Type, amount, currentPrice - amount);
    }
}