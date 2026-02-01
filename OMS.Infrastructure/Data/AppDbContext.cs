using Microsoft.EntityFrameworkCore;
using OMS.Enums;
using OMS.Infrastructure.Entities;
using OMS.Infrastructure.Entities.Customer;

namespace OMS.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<OrderEntity> Orders => Set<OrderEntity>();
        public DbSet<AppliedDiscountEntity> AppliedDiscounts => Set<AppliedDiscountEntity>();
        public DbSet<DiscountCategoryEntity> DiscountCategories => Set<DiscountCategoryEntity>();

        public DbSet<CustomerEntity> Customers => Set<CustomerEntity>();
        public DbSet<CustomFieldEntity> CustomFieldDefinitions => Set<CustomFieldEntity>();
        public DbSet<CustomFieldOptionEntity> CustomFieldOptions => Set<CustomFieldOptionEntity>();
        public DbSet<CustomFieldValueEntity> CustomerCustomFieldValues => Set<CustomFieldValueEntity>();
        public DbSet<CustomerFieldValueHistoryEntity> CustomerFieldValueHistory => Set<CustomerFieldValueHistoryEntity>();

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

            // --- Part 2: Customer and dynamic custom fields ---
            modelBuilder.Entity<CustomFieldEntity>()
                .HasMany(f => f.Options)
                .WithOne(o => o.CustomFieldDefinition)
                .HasForeignKey(o => o.CustomFieldDefinitionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CustomFieldValueEntity>()
                .HasOne(v => v.Customer)
                .WithMany(c => c.CustomFieldValues)
                .HasForeignKey(v => v.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CustomFieldValueEntity>()
                .HasOne(v => v.CustomFieldDefinition)
                .WithMany(f => f.CustomerValues)
                .HasForeignKey(v => v.CustomFieldDefinitionId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<CustomFieldValueEntity>()
                .HasIndex(v => new { v.CustomerId, v.CustomFieldDefinitionId })
                .IsUnique();

            modelBuilder.Entity<CustomerFieldValueHistoryEntity>()
                .HasOne(h => h.Customer)
                .WithMany(c => c.FieldValueHistory)
                .HasForeignKey(h => h.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CustomerFieldValueHistoryEntity>()
                .HasOne(h => h.CustomFieldDefinition)
                .WithMany(f => f.ValueHistory)
                .HasForeignKey(h => h.CustomFieldDefinitionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}