using Smartwyre.DeveloperTest.RebateCalculator;

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
    private RebateCalculator.RebateCalculator _calculator;

    public decimal? Calculate(Product product, decimal volume)
    {
        if (_calculator == null) return null;

        return _calculator.Calculate(this, product, volume);
    }

    private static RebateCalculator.RebateCalculator GetCalculatorFor(IncentiveType incentiveType) => incentiveType switch
    {
        IncentiveType.FixedCashAmount => new FixedCashAmountRebateCalculator(),
        IncentiveType.FixedRateRebate => new FixedRateRebateCalculator(),
        IncentiveType.AmountPerUom => new AmountPerUomRebateCalculator(),
        _ => null
    };
}
