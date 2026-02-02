using OMS.Domain.Models;
using OMS.Models;
using Xunit;

namespace OMS.Tests;

public class DiscountCalculationTests
{
    [Fact]
    public void ApplyDiscount_SubtractsAmount_AndRoundsToTwoDecimals()
    {
        var order = new Order(Guid.NewGuid(), Guid.NewGuid(), 100.00m);
        var calculation = new DiscountCalculation(order);

        calculation.ApplyDiscount(10.555m);

        Assert.Equal(89.44m, calculation.CurrentPrice);
    }

    [Fact]
    public void ApplyDiscount_DoesNotGoBelowZero()
    {
        var order = new Order(Guid.NewGuid(), Guid.NewGuid(), 10.00m);
        var calculation = new DiscountCalculation(order);

        calculation.ApplyDiscount(50.00m);

        Assert.Equal(0m, calculation.CurrentPrice);
    }
}
