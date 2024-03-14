using System;
using Microsoft.Extensions.DependencyInjection;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Runner;

class Program
{
    static void Main(string[] args)
    {
        var serviceProvider = Setup();
        var rebateService = serviceProvider.GetService<IRebateService>();

        var request = new CalculateRebateRequest()
        {
            RebateIdentifier = TryGetIdentifier(args, 0),
            ProductIdentifier = TryGetIdentifier(args, 1),
            Volume = TryGetDecimal(args, 2)
        };

        var result = rebateService.Calculate(request);

        if (result.Success)
        {
            Console.WriteLine($"Calculated value for rebate: {result.RebateAmount}");
        }
    }

    private static string TryGetIdentifier(string[] args, int index)
    {
        try
        {
            return args[index];
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }

    private static decimal TryGetDecimal(string[] args, int index)
    {
        try
        {
            return decimal.Parse(args[index]);
        }
        catch (Exception)
        {
            return 0;
        }
    }

    private static ServiceProvider Setup()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddScoped<IProductDataStore, ProductDataStore>();
        serviceCollection.AddScoped<IRebateDataStore, RebateDataStore>();
        serviceCollection.AddScoped<IRebateService, RebateService>();

        return serviceCollection.BuildServiceProvider();
    }
}
