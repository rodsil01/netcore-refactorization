using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class FixedRateDebateCalculaotr : RebateCalculator
{
    protected override decimal CalculateRebate(Rebate rebate, Product product, decimal volume)
    {
        return product.Price * rebate.Percentage * volume;
    }

    protected override bool IsValid(Rebate rebate, Product product, decimal volume)
    {
        return rebate  != null && 
               product != null && 
               product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedRateRebate) &&
               rebate.Percentage != 0 &&
               product.Price != 0 &&
               volume != 0;
    }
}
