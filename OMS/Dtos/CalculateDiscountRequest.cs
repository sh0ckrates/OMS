namespace OMS.Dtos
{
    public class CalculateDiscountRequest
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public decimal BasePrice { get; set; }
    }
}
