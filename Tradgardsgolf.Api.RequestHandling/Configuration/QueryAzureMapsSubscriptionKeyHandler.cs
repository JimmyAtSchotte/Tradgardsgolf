using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using Tradgardsgolf.Contracts.Settings;
using Tradgardsgolf.Core.Config;

namespace Tradgardsgolf.Api.RequestHandling.Configuration;

[SuppressMessage("ReSharper", "UnusedType.Global")]
public class QueryAzureMapsSubscriptionKeyHandler : IRequestHandler<QueryAzureMapsSubscriptionKey, AzureMapsSubscriptionKeyResponse>
{
    private readonly IOptionsMonitor<AzureMapsSubscriptionKey> _optionsMonitor;
    
    public QueryAzureMapsSubscriptionKeyHandler(IOptionsMonitor<AzureMapsSubscriptionKey> optionsMonitor)
    {
        _optionsMonitor = optionsMonitor;
    }

    public Task<AzureMapsSubscriptionKeyResponse> Handle(QueryAzureMapsSubscriptionKey request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new AzureMapsSubscriptionKeyResponse
        {
            Key = _optionsMonitor.CurrentValue.Value
        });
    }
}