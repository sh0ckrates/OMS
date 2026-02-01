using Application.Services.Interfaces;
using Application.Services.Interfaces.Repo;
using OMS.Domain.Models;
using OMS.Enums;

namespace Application.Services.Concrete;

public sealed class PromotionDiscountPolicy : IDiscountPolicy
{
    private readonly IDiscountCategoryRepository _repo;
    public PromotionDiscountPolicy(IDiscountCategoryRepository repo) => _repo = repo;

    public string Name => "Promotion";
    public int Priority => 2;

    public async Task<bool> IsEligibleAsync(DiscountContext context, CancellationToken ct = default)
        => (await _repo.GetActiveByNameAsync(Name, ct)) is not null && context.CurrentPrice > 0m;

    public async Task<DiscountResult?> GetDiscountAsync(DiscountContext context, CancellationToken ct = default)
    {
        var cat = await _repo.GetActiveByNameAsync(Name, ct);
        if (cat is null) return null;

        var amount = cat.Type == DiscountType.Percentage ? context.CurrentPrice * cat.Value : cat.Value;
        return amount <= 0m ? null : new DiscountResult(cat.Name, cat.Type, amount, context.CurrentPrice);
    }
}