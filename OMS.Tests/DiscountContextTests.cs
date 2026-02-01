using OMS.Domain.Models;
using OMS.Models;
using Xunit;

namespace OMS.Tests;

public class DiscountContextTests
{
    [Fact]
    public void ApplyDiscount_SubtractsAmount_AndRoundsToTwoDecimals()
    {
        var order = new Order(Guid.NewGuid(), Guid.NewGuid(), 100.00m);
        var context = new DiscountContext(order);

        context.ApplyDiscount(10.555m);

        Assert.Equal(89.45m, context.CurrentPrice);
    }

    [Fact]
    public void ApplyDiscount_DoesNotGoBelowZero()
    {
        var order = new Order(Guid.NewGuid(), Guid.NewGuid(), 10.00m);
        var context = new DiscountContext(order);

        context.ApplyDiscount(50.00m);

        Assert.Equal(0m, context.CurrentPrice);
    }
}
