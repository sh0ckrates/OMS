// OMS.Application/Services/Interfaces/IDiscountPolicy.cs
using System.Threading;
using System.Threading.Tasks;
using OMS.Domain.Models;

namespace Application.Services.Interfaces
{
    public interface IDiscountPolicy
    {
        /// <summary>Name + Kind + Value + Priority live in the backing category (infra).</summary>
        string Name { get; }          // e.g. "PriceList"
        int Priority { get; }         // e.g. 1 for PriceList
        Task<bool> IsEligibleAsync(DiscountContext context, CancellationToken ct = default);
        Task<DiscountResult?> GetDiscountAsync(DiscountContext context, CancellationToken ct = default);
    }
}