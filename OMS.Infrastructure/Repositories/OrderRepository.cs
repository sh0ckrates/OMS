using Application.Services.Interfaces.Db;
using Microsoft.EntityFrameworkCore;
using OMS.Domain.Models;
using OMS.Infrastructure.Data;
using OMS.Infrastructure.Entities;
using OMS.Models;

namespace Infrastructure.Repositories
{
    public class OrderRepository(AppDbContext context) : IOrderRepository
    {
        public async Task SaveAsync(Order order)
        {
            // Map Domain Order -> EF OrderEntity
            var orderEntity = new OrderEntity
            {
                Id = order.Id != Guid.Empty ? order.Id : Guid.NewGuid(),
                CustomerId = order.CustomerId,
                BasePrice = order.InitialPrice,
                FinalPrice = order.FinalPrice,
                CreatedAt = order.CreatedAt,
                AppliedDiscounts = [.. order.AppliedDiscounts.Select(d => new AppliedDiscountEntity
                {
                    Id = d.Id != Guid.Empty ? d.Id : Guid.NewGuid(),
                    DiscountCategoryId = d.Category.Id,
                    DiscountName = d.DiscountName,
                    Amount = d.Amount,
                    PriceAfter = d.PriceAfter,
                    AppliedAt = d.AppliedAt
                })]
            };

            context.Orders.Add(orderEntity);
            await context.SaveChangesAsync();
        }

        public async Task<Order?> GetByIdAsync(Guid orderId)
        {
            var entity = await context.Orders
                .Include(o => o.AppliedDiscounts)
                .ThenInclude(d => d.Category)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (entity == null)
                return null;

            // Map EF entity -> Domain model
            var order = new Order(entity.Id, entity.CustomerId, entity.BasePrice)
            {
                FinalPrice = entity.FinalPrice,
                CreatedAt = entity.CreatedAt
            };

            foreach (var d in entity.AppliedDiscounts)
            {
                order.AppliedDiscounts.Add(new AppliedDiscount(
                    category: new DiscountCategory(),
                    discountName: d.DiscountName,
                    amount: d.Amount,
                    priceAfter: d.PriceAfter,
                    id: d.Id
                ));
            }

            return order;
        }
    }
}
