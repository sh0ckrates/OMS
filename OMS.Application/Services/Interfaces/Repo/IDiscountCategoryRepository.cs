using OMS.Models;
using OMS.Models.OMS.Domain.Models;

namespace OMS.Application.Services.Interfaces.Repo
{
    public interface IDiscountCategoryRepository
    {
        Task<List<DiscountCategory>> GetActiveDiscountCategories();
    }
}
