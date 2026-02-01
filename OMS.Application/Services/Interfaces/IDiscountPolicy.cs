using OMS.Domain.Models;

namespace Application.Services.Interfaces
{
    public interface IDiscountPolicy
    {
        string Name { get; }
        int Priority { get; set; }
        Task<bool> IsEligibleAsync(DiscountContext context, CancellationToken ct = default);
        Task<DiscountResult?> GetDiscountAsync(DiscountContext context, CancellationToken ct = default);
    }
}