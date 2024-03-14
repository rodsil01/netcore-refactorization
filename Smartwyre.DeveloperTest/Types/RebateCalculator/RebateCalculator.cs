using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public abstract class RebateCalculator
{
    protected abstract bool IsValid(Rebate rebate, Product product, decimal volume);
    protected abstract decimal CalculateRebate(Rebate rebate, Product product, decimal volume);

    public decimal? Calculate(Rebate rebate, Product product, decimal volume)
    {
        if (IsValid(rebate, product, volume))
        {
            return CalculateRebate(rebate, product, volume);
        }

        return null;
    }
}
