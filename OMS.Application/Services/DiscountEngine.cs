using Application.Services.Interfaces;
using OMS.Domain.Models;
using OMS.Models;

namespace OMS.Application.Services
{
    public sealed class DiscountEngine(IEnumerable<IDiscountPolicy> policies) : IDiscountEngine
    {
        public async Task<DiscountSummary> ApplyDiscountAsync(Order order, CancellationToken ct = default)
        {
            static decimal Round2(decimal value) => Math.Round(value, 2, MidpointRounding.ToEven);

            var orderedPolicies = policies.OrderBy(p => p.Priority).ToList();

            var calculation = new DiscountCalculation(order);
            var results = new List<DiscountResult>();

            foreach (var policy in orderedPolicies)
            {
                if (calculation.CurrentPrice <= 0m) break;

                if (!await policy.IsEligibleAsync(calculation.CurrentPrice, ct)) continue;

                var discountResult = await policy.GetDiscountAsync(calculation.CurrentPrice, ct);
                if (discountResult is null) continue;

                var amount = Math.Min(discountResult.AmountApplied, calculation.CurrentPrice);
                amount = Round2(amount);

                calculation.ApplyDiscount(amount);

                results.Add(new DiscountResult(discountResult.Name, discountResult.Kind, amount, calculation.CurrentPrice));
            }

            return new DiscountSummary(order.InitialPrice, calculation.CurrentPrice, results);
        }
    }
}