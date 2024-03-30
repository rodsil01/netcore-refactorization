using Smartwyre.DeveloperTest.RebateCalculator;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class RebateCalculatorService : IRebateCalculatorService
{
    public RebateCalculator.RebateCalculator GetCalculator(IncentiveType incentiveType) => incentiveType switch
    {
        IncentiveType.FixedCashAmount => new FixedCashAmountRebateCalculator(),
        IncentiveType.FixedRateRebate => new FixedRateRebateCalculator(),
        IncentiveType.AmountPerUom => new AmountPerUomRebateCalculator(),
        _ => null
    };
}
