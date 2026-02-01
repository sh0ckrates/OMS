using System.Linq;
using Application.Services.Interfaces;
using Moq;
using OMS.Application.Services;
using OMS.Domain.Models;
using OMS.Enums;
using OMS.Models;
using Xunit;

namespace OMS.Tests;

public class DiscountEngineTests
{
    [Fact]
    public async Task ApplyDiscountAsync_NoPolicies_ReturnsOriginalPrice()
    {
        var engine = new DiscountEngine([]);
        var order = new Order(Guid.NewGuid(), Guid.NewGuid(), 100.00m);

        var summary = await engine.ApplyDiscountAsync(order);

        Assert.Equal(100.00m, summary.OriginalPrice);
        Assert.Equal(100.00m, summary.FinalPrice);
        Assert.Empty(summary.Discounts);
    }

    [Fact]
    public async Task ApplyDiscountAsync_AppliesPoliciesInPriorityOrder()
    {
        var lowPriorityPolicy = CreateMockPolicy("Low", priority: 2, discountAmount: 10m);
        var highPriorityPolicy = CreateMockPolicy("High", priority: 1, discountAmount: 5m);
        var engine = new DiscountEngine([lowPriorityPolicy.Object, highPriorityPolicy.Object]);
        var order = new Order(Guid.NewGuid(), Guid.NewGuid(), 100.00m);

        var summary = await engine.ApplyDiscountAsync(order);

        Assert.Equal(85.00m, summary.FinalPrice);
        Assert.Equal(2, summary.Discounts.Count);
        Assert.Equal("High", summary.Discounts.First().Name);
        Assert.Equal(5m, summary.Discounts.First().AmountApplied);
        Assert.Equal("Low", summary.Discounts.Skip(1).First().Name);
        Assert.Equal(10m, summary.Discounts.Skip(1).First().AmountApplied);
    }

    [Fact]
    public async Task ApplyDiscountAsync_StopsWhenCurrentPriceIsZero()
    {
        var policy = CreateMockPolicy("Big", priority: 1, discountAmount: 1000m);
        var engine = new DiscountEngine([policy.Object]);
        var order = new Order(Guid.NewGuid(), Guid.NewGuid(), 50.00m);

        var summary = await engine.ApplyDiscountAsync(order);

        Assert.Equal(0m, summary.FinalPrice);
        Assert.Single(summary.Discounts);
        Assert.Equal(50.00m, summary.Discounts.First().AmountApplied);
    }

    [Fact]
    public async Task ApplyDiscountAsync_IneligiblePolicy_IsSkipped()
    {
        var policy = new Mock<IDiscountPolicy>();
        policy.Setup(p => p.Name).Returns("Skip");
        policy.Setup(p => p.Priority).Returns(1);
        policy.Setup(p => p.IsEligibleAsync(It.IsAny<DiscountContext>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        var engine = new DiscountEngine(new[] { policy.Object });
        var order = new Order(Guid.NewGuid(), Guid.NewGuid(), 100.00m);

        var summary = await engine.ApplyDiscountAsync(order);

        Assert.Equal(100.00m, summary.FinalPrice);
        Assert.Empty(summary.Discounts);
        policy.Verify(p => p.GetDiscountAsync(It.IsAny<DiscountContext>(), It.IsAny<CancellationToken>()), Times.Never);
    }


    private static Mock<IDiscountPolicy> CreateMockPolicy(string name, int priority, decimal discountAmount)
    {
        var policy = new Mock<IDiscountPolicy>();
        policy.Setup(p => p.Name).Returns(name);
        policy.Setup(p => p.Priority).Returns(priority);
        policy.Setup(p => p.IsEligibleAsync(It.IsAny<DiscountContext>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        policy.Setup(p => p.GetDiscountAsync(It.IsAny<DiscountContext>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((DiscountContext ctx, CancellationToken _) =>
            {
                var amountToApply = Math.Min(discountAmount, ctx.CurrentPrice);
                var priceAfter = ctx.CurrentPrice - amountToApply;
                return new DiscountResult(name, DiscountType.Fixed, amountToApply, priceAfter);
            });

        return policy;
    }
}
