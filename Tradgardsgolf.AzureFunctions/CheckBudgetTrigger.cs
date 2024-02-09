using System;
using System.Linq;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Messaging.EventGrid;
using Azure.ResourceManager;
using Azure.ResourceManager.Consumption;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Tradgardsgolf.AzureFunctions;

public static class CheckBudgetTrigger
{
    [FunctionName("CheckBudgetTrigger")]
    public static async Task RunAsync([TimerTrigger("0 */30 * * * *")] TimerInfo myTimer, ILogger log)
    {
        log.LogInformation($"C# Timer trigger function executed at: {DateTime.UtcNow}");
        
        var client = new ArmClient(new DefaultAzureCredential());
        var subscription = await client.GetDefaultSubscriptionAsync();

        var resourceGroup = subscription.GetResourceGroups().FirstOrDefault(x => x.Data.Name == "tradgardsgolf");
        
        if (resourceGroup is null)
        {
            log.LogInformation($"Resource group was not found!");
            return;
        }
        
        var budgets = client.GetConsumptionBudgets(resourceGroup.Id);
        var budget = budgets.FirstOrDefault(x => x.Data.Name == "budget");

        if (budget is null)
        {
            log.LogInformation($"Budget was not found!");
            return;
        }

        if (budget.Data.CurrentSpend.Amount > budget.Data.Amount)
        {
            log.LogInformation($"Budget is spent! You ruin me!");

            await PublishBudgetSpentEventAsync();
        }
        else
            log.LogInformation($"Budget is fine");
    }
    
    private static async Task PublishBudgetSpentEventAsync()
    {
        var endpoint = Environment.GetEnvironmentVariable("EventGridTopicEndpoint");
        var credential = new DefaultAzureCredential();
        var client = new EventGridPublisherClient(new Uri(endpoint), credential);

        var eventGridEvent = new EventGridEvent(
            subject: "/budgets/budget-spent",
            eventType: "budget.spent",
            dataVersion: "1.0",
            data: new BudgetSpentEventData()
        );

        await client.SendEventAsync(eventGridEvent);
    }

    private class BudgetSpentEventData
    {
        
    }

}