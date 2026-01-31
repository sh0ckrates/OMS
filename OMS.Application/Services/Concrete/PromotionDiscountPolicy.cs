using Application.Services.Interfaces;
using Domain;
using OMS.Models;
using OMS.Models.OMS.Domain.Models;

namespace Application.Services.Concrete
{
    public class PromotionDiscountPolicy : IDiscountPolicy
    {
        public DiscountCategory Category => DiscountCategories.Promotion;

        public bool IsEligible(DiscountContext context)
        {
            // Example logic:
            // Could check active promotions for the customer/order
            return true;
        }

        public DiscountResult GetDiscountResult(DiscountContext context)
        {
            // Example: 10% of current price
            decimal amount = context.CurrentPrice * 0.10m;

            context.ApplyDiscount(amount);

            return new DiscountResult(Category, amount, context.CurrentPrice);
        }
    }

}
