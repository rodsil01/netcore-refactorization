using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.RebateCalculator;

public class FixedRateRebateCalculator : RebateCalculator
{
    internal protected override decimal CalculateRebate(Rebate rebate, Product product, decimal? volume = null)
    {
        return product.Price * rebate.Percentage * volume.Value;
    }

    internal protected override bool IsValid(Rebate rebate, Product product, decimal? volume = null)
    {
        return rebate  != null && 
               product != null && 
               product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedRateRebate) &&
               rebate.Percentage != 0 &&
               product.Price != 0 &&
               volume.HasValue &&
               volume.Value != 0;
    }
}
