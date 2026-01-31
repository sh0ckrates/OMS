using Application.Services.Interfaces;
using OMS.Models;

public class DiscountEngine(IEnumerable<IDiscountPolicy> policies)
{
    private readonly IReadOnlyCollection<IDiscountPolicy> _policies = [.. policies.OrderBy(p => p.Category.Priority)];

    public DiscountSummary ApplyDiscount(Order order)
    {
        var context = new DiscountContext(order);
        var results = new List<DiscountResult>();

        foreach (var policy in _policies)
        {
            if (!policy.IsEligible(context))
                continue;

            var result = policy.GetDiscountResult(context);
            results.Add(result);
        }

        return new DiscountSummary(order.InitialPrice, context.CurrentPrice, results);
    }
}