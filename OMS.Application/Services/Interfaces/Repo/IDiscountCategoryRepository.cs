using OMS.Domain.Models;

namespace Application.Services.Interfaces.Repo
{
    public interface IDiscountCategoryRepository
    {
        Task<IReadOnlyList<DiscountCategory>> GetActiveDiscountCategories(CancellationToken ct = default);
        Task<DiscountCategory> GetActiveDiscountCategoryByName(string name, CancellationToken ct = default);
    }
}