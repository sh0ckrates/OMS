using Application.Services.Interfaces;
using OMS.Models;
using OMS.Models.OMS.Domain.Models;

namespace Application.Services.Concrete
{
    public class PriceListDiscountPolicy : IDiscountPolicy
    {
        public DiscountCategory Category => new DiscountCategory();//DiscountCategories.PriceList;

        public bool IsEligible(DiscountContext context)
        {
            // Example logic:
            // Could check customer tier, contract, or product eligibility
            return true;
        }

        public DiscountResult GetDiscountResult(DiscountContext context)
        {
            // Example: 5% of current price
            decimal amount = context.CurrentPrice * 0.05m;

            context.ApplyDiscount(amount);

            return new DiscountResult(Category, amount, context.CurrentPrice);
        }
    }

}
