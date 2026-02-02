using OMS.Domain.Models;

namespace Application.Services.Interfaces
{
    public interface IDiscountPolicy
    {
        string Name { get; }
        int Priority { get; set; }
        Task<bool> IsEligibleAsync(decimal currentPrice, CancellationToken ct = default);
        Task<DiscountResult> GetDiscountAsync(decimal currentPrice, CancellationToken ct = default);
    }
}