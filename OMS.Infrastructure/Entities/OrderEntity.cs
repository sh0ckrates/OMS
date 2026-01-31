namespace OMS.Infrastructure.Entities
{
    public class OrderEntity
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public decimal BasePrice { get; set; }
        public decimal FinalPrice { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<AppliedDiscountEntity> AppliedDiscounts { get; set; } = new List<AppliedDiscountEntity>();
    }
}
