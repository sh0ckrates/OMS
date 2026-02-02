using System;
using System.Collections.Generic;
using System.Text;

namespace OMS.Infrastructure.Entities
{
    public class AppliedDiscountEntity
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }
        public OrderEntity Order { get; set; }

        public Guid DiscountCategoryId { get; set; }
        public DiscountCategoryEntity Category { get; set; }

        public string DiscountName { get; set; }
        public decimal Amount { get; set; }
        public decimal PriceAfter { get; set; }
        public DateTime AppliedAt { get; set; } = DateTime.UtcNow;
    }

}
