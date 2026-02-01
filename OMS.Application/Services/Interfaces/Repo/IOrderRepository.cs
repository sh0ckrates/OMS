using OMS.Models;

namespace Application.Services.Interfaces.Db
{
    public interface IOrderRepository
    {
        /// <summary>
        /// Saves an order and all applied discounts.
        /// </summary>
        Task SaveAsync(Order order);

        /// <summary>
        /// Optionally: get order by id (used in read scenarios)
        /// </summary>
        Task<Order?> GetByIdAsync(Guid orderId);
    }
}
