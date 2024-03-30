using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService
{
    private readonly IProductDataStore _productDataStore;
    private readonly IRebateDataStore _rebateDataStore;
    private readonly IRebateCalculatorService _rebateCalculatorService;

    public RebateService(IProductDataStore productDataStore, IRebateDataStore rebateDataStore, IRebateCalculatorService rebateCalculatorService)
    {
        _productDataStore = productDataStore;
        _rebateDataStore = rebateDataStore;
        _rebateCalculatorService = rebateCalculatorService;
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

    private CalculateRebateResult CalculateRebate(Rebate rebate, Product product, decimal volume)
    {
        var result = new CalculateRebateResult()
        {
            Success = false
        };

        if (rebate == null) return result;

        var calculator = _rebateCalculatorService.GetCalculator(rebate.Incentive);
        var rebateAmount = calculator.Calculate(rebate, product, volume);

        if (rebateAmount.HasValue)
        {
            result.Success = true;
            result.RebateAmount = rebateAmount.Value;
        }

        return result;   
    }
}
