using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public interface IRebateCalculatorService
{
    RebateCalculator.RebateCalculator GetCalculator(IncentiveType incentiveType);
}
