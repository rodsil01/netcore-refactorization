using Smartwyre.DeveloperTest.Services;

namespace Smartwyre.DeveloperTest.Types;

public class Rebate
{
    public string Identifier { get; set; }
    public IncentiveType Incentive
    { 
        get
        {
            return _incentiveType;
        }
        set
        {
            _incentiveType = value;
            _calculator = GetCalculatorFor(value);
        }
    }

    public decimal Amount { get; set; }
    public decimal Percentage { get; set; }

    private IncentiveType _incentiveType;
    private RebateCalculator _calculator;

    public decimal? Calculate(Product product, decimal volume)
    {
        if (_calculator == null) return null;

        return _calculator.Calculate(this, product, volume);
    }

    private static RebateCalculator GetCalculatorFor(IncentiveType incentiveType) => incentiveType switch
    {
        IncentiveType.FixedCashAmount => new FixedCashDebateCalculaotr(),
        IncentiveType.FixedRateRebate => new FixedRateDebateCalculaotr(),
        IncentiveType.AmountPerUom => new AmountPerUomDebateCalculaotr(),
        _ => null
    };
}
