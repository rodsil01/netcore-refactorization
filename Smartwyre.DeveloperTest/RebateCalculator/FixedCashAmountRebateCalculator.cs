using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.RebateCalculator;

public class FixedCashAmountRebateCalculator : RebateCalculator
{
    internal protected override decimal CalculateRebate(Rebate rebate, Product product, decimal? volume = null)
    {
        return rebate.Amount;
    }

    internal protected override bool IsValid(Rebate rebate, Product product, decimal? volume = null)
    {
        return rebate != null && 
               product != null &&
               product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount) &&
               rebate.Amount != 0;
    }
}
