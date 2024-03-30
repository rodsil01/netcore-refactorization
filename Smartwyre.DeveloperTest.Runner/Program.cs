using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Runner;

class Program
{
    static async Task Main(string[] args)
    {
        var serviceProvider = Setup();
        var rebateService = serviceProvider.GetService<IRebateService>();

        var requests = new List<CalculateRebateRequest>() {
            new()
            {
                RebateIdentifier = TryGetIdentifier(args, 0),
                ProductIdentifier = TryGetIdentifier(args, 1),
                Volume = TryGetDecimal(args, 2)
            },
            new()
            {
                RebateIdentifier = TryGetIdentifier(args, 0),
                ProductIdentifier = TryGetIdentifier(args, 1),
                Volume = TryGetDecimal(args, 2)
            },
            new()
            {
                RebateIdentifier = TryGetIdentifier(args, 0),
                ProductIdentifier = TryGetIdentifier(args, 1),
                Volume = TryGetDecimal(args, 2)
            }
        };

        IEnumerable<CalculateRebateResult> results = await GetRebateResult(rebateService, requests);

        results.ToList().ForEach(r => {
            if (r.Success) {
                Console.WriteLine($"Calculated value for rebate: {r.RebateAmount}");
            }
        });

        Console.WriteLine($"Program finished successfully");
    }

    private static CalculateRebateResult GetRebateResult(IRebateService rebateService, CalculateRebateRequest request) {
        return rebateService.Calculate(request);
    }

    private static async Task<IEnumerable<CalculateRebateResult>> GetRebateResult(IRebateService rebateService, IEnumerable<CalculateRebateRequest> requests) {
        List<Task<CalculateRebateResult>> taks = requests.Select(x => Task.Run(() => rebateService.Calculate(x))).ToList();
        return await Task.WhenAll(taks);
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
        serviceCollection.AddScoped<IRebateCalculatorService, RebateCalculatorService>();

        return serviceCollection.BuildServiceProvider();
    }
}
