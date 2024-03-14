using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService
{
    private readonly IProductDataStore _productDataStore;
    private readonly IRebateDataStore _rebateDataStore;

    public RebateService(IProductDataStore productDataStore, IRebateDataStore rebateDataStore)
    {
        _productDataStore = productDataStore;
        _rebateDataStore = rebateDataStore;
    }

    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        Rebate rebate = _rebateDataStore.GetRebate(request.RebateIdentifier);
        Product product = _productDataStore.GetProduct(request.ProductIdentifier);

        CalculateRebateResult result = CalculateRebate(rebate, product, request.Volume);

        if (result.Success)
        {
            _rebateDataStore.StoreCalculationResult(rebate, result.RebateAmount);
        }

        return result;
    }

    private static CalculateRebateResult CalculateRebate(Rebate rebate, Product product, decimal volume)
    {
        var result = new CalculateRebateResult()
        {
            Success = false
        };

        if (rebate == null) return result;

        var rebateAmount = rebate.Calculate(product, volume);

        if (rebateAmount.HasValue)
        {
            result.Success = true;
            result.RebateAmount = rebateAmount.Value;
        }

        return result;   
    }
}
