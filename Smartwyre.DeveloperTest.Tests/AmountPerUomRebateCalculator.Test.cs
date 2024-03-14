using Smartwyre.DeveloperTest.RebateCalculator;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests;

public class AmountPerUomRebateCalculatorTests
{
    private readonly AmountPerUomRebateCalculator _calculator;

    public AmountPerUomRebateCalculatorTests()
    {
        _calculator = new AmountPerUomRebateCalculator();
    }

    [Fact]
    public void CalculateRebate_IsValid_ReturnsAmount()
    {
        Rebate rebate = new() { Amount = 20m };
        Product product = new() { SupportedIncentives = SupportedIncentiveType.AmountPerUom };
        decimal volume = 5;

        decimal? amount = _calculator.Calculate(rebate, product, volume);

        Assert.True(amount.HasValue);
        Assert.Equal(rebate.Amount * volume, amount.Value);
    }

    [Fact]
    public void CalculateRebate_IsNotValid_ReturnsNull()
    {
        Rebate rebate = null;
        Product product = new() { SupportedIncentives = SupportedIncentiveType.AmountPerUom };
        decimal volume = 5;

        decimal? amount = _calculator.Calculate(rebate, product, volume);

        Assert.False(amount.HasValue);
    }

    [Fact]
    public void IsValid_AllInputsValid_ReturnsTrue()
    {
        Rebate rebate = new() { Amount = 20m };
        Product product = new() { SupportedIncentives = SupportedIncentiveType.AmountPerUom };
        decimal volume = 5;

        bool isValid = _calculator.IsValid(rebate, product, volume);

        Assert.True(isValid);
    }

    [Fact]
    public void IsValid_RebateIsNull_ReturnsFalse()
    {
        Rebate rebate = null;
        Product product = new() { SupportedIncentives = SupportedIncentiveType.AmountPerUom };
        decimal volume = 5;

        bool isValid = _calculator.IsValid(rebate, product, volume);

        Assert.False(isValid);
    }

    [Fact]
    public void IsValid_ProductIsNull_ReturnsFalse()
    {
        Rebate rebate = new() { Amount = 20m };
        Product product = null;
        decimal volume = 5;

        bool isValid = _calculator.IsValid(rebate, product, volume);

        Assert.False(isValid);
    }

    [Fact]
    public void IsValid_InvalidSupportedIncentives_ReturnsFalse()
    {
        Rebate rebate = new() { Amount = 20m };
        Product product = new() { SupportedIncentives = SupportedIncentiveType.FixedCashAmount };
        decimal volume = 5;

        bool isValid = _calculator.IsValid(rebate, product, volume);

        Assert.False(isValid);
    }

    [Fact]
    public void IsValid_AmountIsZero_ReturnsFalse()
    {
        Rebate rebate = new() { Amount = 0 };
        Product product = new() { SupportedIncentives = SupportedIncentiveType.AmountPerUom };
        decimal volume = 5;

        bool isValid = _calculator.IsValid(rebate, product, volume);

        Assert.False(isValid);
    }

    [Fact]
    public void IsValid_VolumeIsZero_ReturnsFalse()
    {
        Rebate rebate = new() { Amount = 20m };
        Product product = new() { SupportedIncentives = SupportedIncentiveType.AmountPerUom };
        decimal volume = 0;

        bool isValid = _calculator.IsValid(rebate, product, volume);

        Assert.False(isValid);
    }

    [Fact]
    public void IsValid_VolumeIsNull_ReturnsFalse()
    {
        Rebate rebate = new() { Amount = 20m };
        Product product = new() { SupportedIncentives = SupportedIncentiveType.AmountPerUom };

        bool isValid = _calculator.IsValid(rebate, product);

        Assert.False(isValid);
    }
}
