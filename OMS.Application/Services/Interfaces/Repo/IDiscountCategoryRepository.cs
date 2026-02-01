using OMS.Domain.Models;
using OMS.Models.OMS.Domain.Models;

namespace Application.Services.Interfaces.Repo
{
    /// <summary>
    /// Pure application-facing contract. No EFCore here. Returns DOMAIN models.
    /// </summary>
    public interface IDiscountCategoryRepository
    {
        Task<IReadOnlyList<DiscountCategory>> GetActiveAsync(CancellationToken ct = default);
        Task<DiscountCategory?> GetActiveByNameAsync(string name, CancellationToken ct = default);
    }
}