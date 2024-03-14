using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.RebateCalculator;

public abstract class RebateCalculator
{
    internal protected abstract bool IsValid(Rebate rebate, Product product, decimal? volume = null);
    internal protected abstract decimal CalculateRebate(Rebate rebate, Product product, decimal? volume = null);

    public decimal? Calculate(Rebate rebate, Product product, decimal? volume = null)
    {
        if (IsValid(rebate, product, volume))
        {
            return CalculateRebate(rebate, product, volume);
        }

        return null;
    }
}
