using Application.Services.Interfaces;
using OMS.Domain.Models;
using OMS.Models;

namespace Application.Services.Concrete
{
    public sealed class DiscountEngine(IEnumerable<IDiscountPolicy> policies) : IDiscountEngine
    {
        private readonly IReadOnlyList<IDiscountPolicy> _policies = policies.OrderBy(p => p.Priority).ToList();

        public async Task<DiscountSummary> ApplyDiscountAsync(Order order, CancellationToken ct = default)
        {
            static decimal Round2(decimal v) => Math.Round(v, 2, MidpointRounding.ToEven);

            var context = new DiscountContext(order);
            var results = new List<DiscountResult>();

            foreach (var policy in _policies)
            {
                if (context.CurrentPrice <= 0m) break;

                if (!await policy.IsEligibleAsync(context, ct)) continue;

                var proposal = await policy.GetDiscountAsync(context, ct);
                if (proposal is null) continue;

                var amount = Math.Min(proposal.AmountApplied, context.CurrentPrice);
                amount = Round2(amount);

                context.ApplyDiscount(amount); // rounds & clamps to 0

                results.Add(new DiscountResult(proposal.Name, proposal.Kind, amount, context.CurrentPrice));
            }

            return new DiscountSummary(order.InitialPrice, context.CurrentPrice, results);
        }
    }
}