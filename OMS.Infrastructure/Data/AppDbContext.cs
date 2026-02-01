// OMS.Infrastructure/Data/AppDbContext.cs
using Microsoft.EntityFrameworkCore;
using OMS.Enums;
using OMS.Infrastructure.Entities;

namespace OMS.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<OrderEntity> Orders => Set<OrderEntity>();
        public DbSet<AppliedDiscountEntity> AppliedDiscounts => Set<AppliedDiscountEntity>();
        public DbSet<DiscountCategoryEntity> DiscountCategories => Set<DiscountCategoryEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderEntity>()
                .HasMany(o => o.AppliedDiscounts)
                .WithOne(d => d.Order)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AppliedDiscountEntity>()
                .HasOne(d => d.Category)
                .WithMany(c => c.AppliedDiscounts)
                .HasForeignKey(d => d.DiscountCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // --- Seed the three discount categories ---
            // NOTE: DiscountCategoryEntity must have: Id, Name, Type, Value, Priority, IsActive
            modelBuilder.Entity<DiscountCategoryEntity>().HasData(
                new DiscountCategoryEntity
                {
                    Id = Guid.Parse("d1a2f3b4-c5d6-47e8-9f0a-1234567890ab"),
                    Name = "PriceList",
                    Type = DiscountType.Percentage, // 5% of running price
                    Value = 0.05m,
                    Priority = 1,
                    IsActive = true
                },
                new DiscountCategoryEntity
                {
                    Id = Guid.Parse("e2b3c4d5-f6a7-48b9-0c1d-2345678901bc"),
                    Name = "Promotion",
                    Type = DiscountType.Percentage, // 10% of running price
                    Value = 0.10m,
                    Priority = 2,
                    IsActive = true
                },
                new DiscountCategoryEntity
                {
                    Id = Guid.Parse("f3c4d5e6-a7b8-49c0-1d2e-3456789012cd"),
                    Name = "Coupon",
                    Type = DiscountType.Fixed,      // flat €10
                    Value = 10.00m,
                    Priority = 3,
                    IsActive = true
                }
            );
        }
    }
}