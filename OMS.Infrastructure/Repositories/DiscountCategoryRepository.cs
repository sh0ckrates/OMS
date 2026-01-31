using Microsoft.EntityFrameworkCore;
using OMS.Application.Services.Interfaces.Repo;
using OMS.Infrastructure.Data;
using OMS.Models;
using OMS.Models.OMS.Domain.Models;

namespace OMS.Infrastructure.Repositories
{
    public class DiscountCategoryRepository(AppDbContext context) : IDiscountCategoryRepository
    {
        public async Task<List<DiscountCategory>> GetActiveDiscountCategories()
        {
            return await context.DiscountCategories
                .Where(c => c.IsActive)
                .OrderBy(c => c.Priority)
                .Select(c => new DiscountCategory
                {
                    Id = c.Id,
                    Name = c.Name,
                    Type = c.Type,
                    Priority = c.Priority,
                    IsActive = c.IsActive
                })
                .ToListAsync();
        }
    }
}
