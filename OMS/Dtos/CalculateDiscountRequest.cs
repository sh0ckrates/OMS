using System.ComponentModel.DataAnnotations;

namespace OMS.Dtos
{
    public class CalculateDiscountRequest
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Base price must be greater than zero")]
        public decimal BasePrice { get; set; }
    }
}
