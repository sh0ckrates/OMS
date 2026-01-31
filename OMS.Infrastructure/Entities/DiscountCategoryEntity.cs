using OMS.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OMS.Infrastructure.Entities
{
    public class DiscountCategoryEntity
    {
        public Guid Id { get; set; }             // PK
        public string Name { get; set; }         // e.g. "PriceList", "Promotion", "Coupon"
        public DiscountType Type { get; set; }   // Percentage / FixedAmount
        public int Priority { get; set; }        // 1=highest (PriceList)
        public bool IsActive { get; set; } = true;

        public ICollection<AppliedDiscountEntity> AppliedDiscounts { get; set; } = new List<AppliedDiscountEntity>();
    }

}
