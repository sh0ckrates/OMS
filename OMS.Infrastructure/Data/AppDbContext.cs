using Microsoft.EntityFrameworkCore;
using OMS.Enums;
using OMS.Infrastructure.Entities;

namespace OMS.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<AppliedDiscountEntity> AppliedDiscounts { get; set; }
        public DbSet<DiscountCategoryEntity> DiscountCategories { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DiscountCategoryEntity>().HasData(
                new DiscountCategoryEntity
                {
                    Id = Guid.Parse("d1a2f3b4-c5d6-47e8-9f0a-1234567890ab"),
                    Name = "PriceList",
                    Type = DiscountType.Percentage,
                    Priority = 1,
                    IsActive = true
                },
                new DiscountCategoryEntity
                {
                    Id = Guid.Parse("e2b3c4d5-f6a7-48b9-0c1d-2345678901bc"),
                    Name = "Promotion",
                    Type = DiscountType.Percentage,
                    Priority = 2,
                    IsActive = true
                },
                new DiscountCategoryEntity
                {
                    Id = Guid.Parse("f3c4d5e6-a7b8-49c0-1d2e-3456789012cd"),
                    Name = "Coupon",
                    Type = DiscountType.Fixed,
                    Priority = 3,
                    IsActive = true
                }
            );
        }

    }
}
