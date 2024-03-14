using Smartwyre.DeveloperTest.RebateCalculator;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests;

public class FixedCashAmountRebateCalculatorTests
{
    private readonly FixedCashAmountRebateCalculator _calculator;

    public FixedCashAmountRebateCalculatorTests()
    {
        _calculator = new FixedCashAmountRebateCalculator();
    }

    [Fact]
    public void CalculateRebate_IsValid_ReturnsAmount()
    {
        Rebate rebate = new() { Amount = 20m };
        Product product = new() { SupportedIncentives = SupportedIncentiveType.FixedCashAmount };

        decimal? amount = _calculator.Calculate(rebate, product);

        Assert.True(amount.HasValue);
        Assert.Equal(rebate.Amount, amount.Value);
    }

    [Fact]
    public void CalculateRebate_IsNotValid_ReturnsNull()
    {
        Rebate rebate = null;
        Product product = new() { SupportedIncentives = SupportedIncentiveType.FixedCashAmount };

        decimal? amount = _calculator.Calculate(rebate, product);

        Assert.False(amount.HasValue);
    }

    [Fact]
    public void IsValid_AllInputsValid_ReturnsTrue()
    {
        Rebate rebate = new() { Amount = 20m };
        Product product = new() { SupportedIncentives = SupportedIncentiveType.FixedCashAmount };

        bool isValid = _calculator.IsValid(rebate, product);

        Assert.True(isValid);
    }

    [Fact]
    public void IsValid_RebateIsNull_ReturnsFalse()
    {
        Rebate rebate = null;
        Product product = new() { SupportedIncentives = SupportedIncentiveType.FixedCashAmount };

        bool isValid = _calculator.IsValid(rebate, product);

        Assert.False(isValid);
    }

    [Fact]
    public void IsValid_ProductIsNull_ReturnsFalse()
    {
        Rebate rebate = new() { Amount = 20m };
        Product product = null;

        bool isValid = _calculator.IsValid(rebate, product);

        Assert.False(isValid);
    }

    [Fact]
    public void IsValid_InvalidSupportedIncentives_ReturnsFalse()
    {
        Rebate rebate = new() { Amount = 20m };
        Product product = new() { SupportedIncentives = SupportedIncentiveType.FixedRateRebate };

        bool isValid = _calculator.IsValid(rebate, product);

        Assert.False(isValid);
    }

    [Fact]
    public void IsValid_AmountIsZero_ReturnsFalse()
    {
        Rebate rebate = new() { Amount = 0 };
        Product product = new() { SupportedIncentives = SupportedIncentiveType.FixedCashAmount };

        bool isValid = _calculator.IsValid(rebate, product);

        Assert.False(isValid);
    }
}
