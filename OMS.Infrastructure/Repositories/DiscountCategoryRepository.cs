using Application.Services.Interfaces.Repo;
using Microsoft.EntityFrameworkCore;
using OMS.Infrastructure.Data;
using OMS.Infrastructure.Entities;
using OMS.Domain.Models;

namespace Infrastructure.Repositories
{
    public sealed class DiscountCategoryRepository(AppDbContext db) : IDiscountCategoryRepository
    {
        public async Task<IReadOnlyList<DiscountCategory>> GetActiveAsync(CancellationToken ct = default)
        {
            var rows = await db.DiscountCategories
                .AsNoTracking()
                .Where(c => c.IsActive)
                .OrderBy(c => c.Priority)
                .ToListAsync(ct);

            return rows.Select(MapToDomain).ToList();
        }

        public async Task<DiscountCategory?> GetActiveByNameAsync(string name, CancellationToken ct = default)
        {
            var row = await db.DiscountCategories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.IsActive && c.Name == name, ct);

            return row is null ? null : MapToDomain(row);
        }

        private static DiscountCategory MapToDomain(DiscountCategoryEntity e)
        {
            // Mirror the semantics that matter in the domain
            var model = new DiscountCategory(
                name: e.Name,
                type: e.Type,
                value: e.Value,          // Percentage (0..1) or Fixed amount
                priority: e.Priority,
                isActive: e.IsActive
            )
            {
                Id = e.Id
            };

            return model;
        }
    }
}