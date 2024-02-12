// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName=ShutDownApi

using System;
using System.Linq;
using System.Threading.Tasks;
using Azure.Identity;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Azure.Messaging.EventGrid;
using Azure.ResourceManager;
using Azure.ResourceManager.AppService;
using Microsoft.Azure.Functions.Worker;

namespace Tradgardsgolf.AzureFunctions;

public static class ShutDownApi
{
    [FunctionName("ShutDownApi")]
    public static async Task RunAsync([EventGridTrigger()] EventGridEvent eventGridEvent, ILogger log)
    {
        var client = new ArmClient(new DefaultAzureCredential());
        var subscription = await client.GetDefaultSubscriptionAsync();
        
        var resourceGroup = subscription
            .GetResourceGroups()
            .FirstOrDefault(x => x.Data.Name == "tradgardsgolf-api");

        var api = client.GetWebSiteResource(resourceGroup.Id);
        await api.StopAsync();
      
        log.LogInformation($"Api has stopped");
    }
}