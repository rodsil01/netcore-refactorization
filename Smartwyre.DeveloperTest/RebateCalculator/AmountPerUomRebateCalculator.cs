using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.RebateCalculator;

public class AmountPerUomRebateCalculator : RebateCalculator
{
    internal protected override decimal CalculateRebate(Rebate rebate, Product product, decimal? volume = null)
    {
        return rebate.Amount * volume.Value;
    }

    internal protected override bool IsValid(Rebate rebate, Product product, decimal? volume = null)
    {
        return rebate  != null &&
               product != null &&
               product.SupportedIncentives.HasFlag(SupportedIncentiveType.AmountPerUom) &&
               rebate.Amount != 0 &&
               volume.HasValue &&
               volume.Value != 0;
    }
}
