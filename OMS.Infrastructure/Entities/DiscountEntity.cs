namespace OMS.Infrastructure.Entities
{
    public class DiscountEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public Guid OrderId { get; set; }
        public OrderEntity Order { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
