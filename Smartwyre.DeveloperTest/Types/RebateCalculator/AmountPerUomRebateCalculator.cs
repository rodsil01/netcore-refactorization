using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class AmountPerUomDebateCalculaotr : RebateCalculator
{
    protected override decimal CalculateRebate(Rebate rebate, Product product, decimal volume)
    {
        return rebate.Amount * volume;
    }

    protected override bool IsValid(Rebate rebate, Product product, decimal volume)
    {
        return rebate  != null &&
               product != null &&
               product.SupportedIncentives.HasFlag(SupportedIncentiveType.AmountPerUom) &&
               rebate.Amount != 0 &&
               volume != 0;
    }
}
