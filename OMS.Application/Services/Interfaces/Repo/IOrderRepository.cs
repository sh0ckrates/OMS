using OMS.Models;

namespace Application.Services.Interfaces.Db
{
    public interface IOrderRepository
    {
        Task SaveAsync(Order order);
        Task<Order> GetByIdAsync(Guid orderId);
    }
}
