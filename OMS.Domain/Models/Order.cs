using OMS.Domain.Models;

namespace OMS.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public decimal InitialPrice { get; set; }
        public decimal FinalPrice { get; set; }  // after discounts
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Applied discounts
        public List<AppliedDiscount> AppliedDiscounts { get; set; } = new();

        // Constructor
        public Order(Guid id, Guid customerId, decimal initialPrice)
        {
            Id = id != Guid.Empty ? id : Guid.NewGuid();
            CustomerId = customerId;
            InitialPrice = initialPrice;
            FinalPrice = initialPrice;
        }

        public Order() { }
    }
}
