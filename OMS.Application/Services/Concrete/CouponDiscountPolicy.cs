using Application.Services.Interfaces;
using OMS.Enums;
using OMS.Models;
using OMS.Models.OMS.Domain.Models;

namespace Application.Services.Concrete
{

    namespace OMS.Domain.Policies
    {
        public class CouponDiscountPolicy(DiscountCategory category) : IDiscountPolicy
        {
            private readonly DiscountCategory _category = category ?? throw new ArgumentNullException(nameof(category)); // injected from DB

            public DiscountCategory Category => _category;

            /// <summary>
            /// Determine if the coupon applies to this order.
            /// You can inject additional services (like repository) if needed.
            /// </summary>
            public bool IsEligible(DiscountContext context)
            {
                // TODO: implement real eligibility logic (check customer, order, coupon validity)
                return true;
            }

            public DiscountResult GetDiscountResult(DiscountContext context)
            {
                decimal amount;

                if (_category.Type == DiscountType.Fixed)
                    amount = _category.Value;                  // e.g., 10€
                else // Percentage
                    amount = context.CurrentPrice * (_category.Value / 100m); // e.g., 5% of current price

                context.ApplyDiscount(amount);

                return new DiscountResult(_category, amount, context.CurrentPrice);
            }
        }
    }

}
