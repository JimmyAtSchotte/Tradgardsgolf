using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using Tradgardsgolf.Contracts.Settings;
using Tradgardsgolf.Core.Config;

namespace Tradgardsgolf.Api.RequestHandling.Configuration;

public class AzureMapsSubscriptionKeyHandler : IRequestHandler<AzureMapsSubscriptionKeyCommand, AzureMapsSubscriptionKeyResponse>
{
    private readonly IOptionsMonitor<AzureMapsSubscriptionKey> _optionsMonitor;
    
    public AzureMapsSubscriptionKeyHandler(IOptionsMonitor<AzureMapsSubscriptionKey> optionsMonitor)
    {
        _optionsMonitor = optionsMonitor;
    }

    public Task<AzureMapsSubscriptionKeyResponse> Handle(AzureMapsSubscriptionKeyCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new AzureMapsSubscriptionKeyResponse()
        {
            Key = _optionsMonitor.CurrentValue.Value
        });
    }
}