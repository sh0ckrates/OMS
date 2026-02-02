using OMS.Domain.Models;

namespace OMS.Models
{
    public class Order(Guid id, Guid customerId, decimal initialPrice)
    {
        public Guid Id { get; set; } = id != Guid.Empty ? id : Guid.NewGuid();
        public Guid CustomerId { get; set; } = customerId;
        public decimal InitialPrice { get; set; } = initialPrice;
        public decimal FinalPrice { get; set; } = initialPrice;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<AppliedDiscount> AppliedDiscounts { get; set; } = [];
    }
}
