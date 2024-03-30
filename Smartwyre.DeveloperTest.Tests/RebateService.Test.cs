using Moq;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests;

public class RebateServiceTests
{
    private readonly Mock<IProductDataStore> _productMockDataStore;
    private readonly Mock<IRebateDataStore> _rebateMockDataStore;

    private readonly IRebateCalculatorService _rebateCalculatorService;
    private readonly RebateService _rebateService;

    public RebateServiceTests()
    {
        _productMockDataStore = new Mock<IProductDataStore>();
        _rebateMockDataStore = new Mock<IRebateDataStore>();
        _rebateCalculatorService = new RebateCalculatorService();

        _productMockDataStore.Setup(m => m.GetProduct("product-id")).Returns(new Product
        {
            Id = 1,
            Identifier = "product-id",
            Price = 5,
            Uom = "uom-value",
            SupportedIncentives = SupportedIncentiveType.FixedCashAmount
        });

        _rebateMockDataStore.Setup(m => m.GetRebate("rebate-id")).Returns(new Rebate
        {
            Identifier = "rebate-id",
            Amount = 5,
            Percentage = 0.1m,
            Incentive = IncentiveType.FixedCashAmount
        });

        _rebateService = new RebateService(_productMockDataStore.Object, _rebateMockDataStore.Object, _rebateCalculatorService);
    }

    [Fact]
    public void Calculate_IsValid_ReturnsSuccess()
    {
        CalculateRebateRequest request = new()
        {
            RebateIdentifier = "rebate-id",
            ProductIdentifier = "product-id",
            Volume = 5
        };

        CalculateRebateResult result = _rebateService.Calculate(request);

        _rebateMockDataStore.Verify(x => x.StoreCalculationResult(It.IsAny<Rebate>(), It.IsAny<decimal>()), Times.Once);
        Assert.True(result.Success);
    }

    [Fact]
    public void Calculate_IsNotValid_ReturnsFailure()
    {
        CalculateRebateRequest request = new()
        {
            RebateIdentifier = "empty",
            ProductIdentifier = "empty",
            Volume = 5
        };

        CalculateRebateResult result = _rebateService.Calculate(request);

        _rebateMockDataStore.Verify(x => x.StoreCalculationResult(It.IsAny<Rebate>(), It.IsAny<decimal>()), Times.Never);
        Assert.False(result.Success);
    }
}
