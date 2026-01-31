using OMS.Models;
using OMS.Models.OMS.Domain.Models;

namespace Application.Services.Interfaces
{
    public interface IDiscountPolicy
    {
        DiscountCategory Category { get; }
        bool IsEligible(DiscountContext context);
        DiscountResult GetDiscountResult(DiscountContext context);
    }
}
