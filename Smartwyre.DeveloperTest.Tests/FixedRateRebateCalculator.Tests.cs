using Smartwyre.DeveloperTest.RebateCalculator;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests;

public class FixedRateRebateCalculatorTests
{
    private readonly FixedRateRebateCalculator _calculator;

    public FixedRateRebateCalculatorTests()
    {
        _calculator = new FixedRateRebateCalculator();
    }

    [Fact]
    public void CalculateRebate_IsValid_ReturnsAmount()
    {
        Rebate rebate = new() { Percentage = 0.1m };
        Product product = new() { SupportedIncentives = SupportedIncentiveType.FixedRateRebate, Price = 100m };
        decimal volume = 5;

        decimal? amount = _calculator.Calculate(rebate, product, volume);

        Assert.True(amount.HasValue);
        Assert.Equal(product.Price * rebate.Percentage * volume, amount.Value);
    }

    [Fact]
    public void CalculateRebate_IsNotValid_ReturnsNull()
    {
        Rebate rebate = null;
        Product product = new() { SupportedIncentives = SupportedIncentiveType.FixedRateRebate, Price = 100m };
        decimal volume = 5;

        decimal? amount = _calculator.Calculate(rebate, product, volume);

        Assert.False(amount.HasValue);
    }

    [Fact]
    public void IsValid_AllInputsValid_ReturnsTrue()
    {
        Rebate rebate = new() { Percentage = 0.1m };
        Product product = new() { SupportedIncentives = SupportedIncentiveType.FixedRateRebate, Price = 100m };
        decimal volume = 5;

        bool isValid = _calculator.IsValid(rebate, product, volume);

        Assert.True(isValid);
    }

    [Fact]
    public void IsValid_RebateIsNull_ReturnsFalse()
    {
        Rebate rebate = null;
        Product product = new() { SupportedIncentives = SupportedIncentiveType.FixedRateRebate, Price = 100m };
        decimal volume = 5;

        bool isValid = _calculator.IsValid(rebate, product, volume);

        Assert.False(isValid);
    }

    [Fact]
    public void IsValid_ProductIsNull_ReturnsFalse()
    {
        Rebate rebate = new() { Percentage = 0.1m };
        Product product = null;
        decimal volume = 5;

        bool isValid = _calculator.IsValid(rebate, product, volume);

        Assert.False(isValid);
    }

    [Fact]
    public void IsValid_InvalidSupportedIncentives_ReturnsFalse()
    {
        Rebate rebate = new() { Percentage = 0.1m };
        Product product = new() { SupportedIncentives = SupportedIncentiveType.FixedCashAmount, Price = 100m };
        decimal volume = 5;

        bool isValid = _calculator.IsValid(rebate, product, volume);

        Assert.False(isValid);
    }

    [Fact]
    public void IsValid_PercentageIsZero_ReturnsFalse()
    {
        Rebate rebate = new() { Percentage = 0 };
        Product product = new() { SupportedIncentives = SupportedIncentiveType.FixedRateRebate, Price = 100m };
        decimal volume = 5;

        bool isValid = _calculator.IsValid(rebate, product, volume);

        Assert.False(isValid);
    }

    [Fact]
    public void IsValid_PriceIsZero_ReturnsFalse()
    {
        Rebate rebate = new() { Percentage = 0.1m };
        Product product = new() { SupportedIncentives = SupportedIncentiveType.FixedRateRebate, Price = 0 };
        decimal volume = 5;

        bool isValid = _calculator.IsValid(rebate, product, volume);

        Assert.False(isValid);
    }

    [Fact]
    public void IsValid_VolumeIsZero_ReturnsFalse()
    {
        Rebate rebate = new() { Percentage = 0.1m };
        Product product = new() { SupportedIncentives = SupportedIncentiveType.FixedRateRebate, Price = 100m };
        decimal volume = 0;

        bool isValid = _calculator.IsValid(rebate, product, volume);

        Assert.False(isValid);
    }

    [Fact]
    public void IsValid_VolumeIsNull_ReturnsFalse()
    {
        Rebate rebate = new() { Percentage = 0.1m };
        Product product = new() { SupportedIncentives = SupportedIncentiveType.FixedRateRebate, Price = 100m };

        bool isValid = _calculator.IsValid(rebate, product);

        Assert.False(isValid);
    }
}
