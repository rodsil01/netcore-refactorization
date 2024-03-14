using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class FixedCashDebateCalculaotr : RebateCalculator
{
    protected override decimal CalculateRebate(Rebate rebate, Product product, decimal volume)
    {
        return rebate.Amount;
    }

    protected override bool IsValid(Rebate rebate, Product product, decimal volume)
    {
        return rebate != null && 
               product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount) &&
               rebate.Amount != 0;
    }
}
