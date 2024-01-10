using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Tradgardsgolf.BlazorWasm.Options;

namespace Tradgardsgolf.BlazorWasm;

public class SubscriptionKeyHandler(IOptions<Backend> backend) : DelegatingHandler
{
    private readonly string _subscriptionKey = Uri.EscapeDataString(backend.Value.Key);

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if(string.IsNullOrEmpty(_subscriptionKey))
            return base.SendAsync(request, cancellationToken);
        
        var uriBuilder = new UriBuilder(request.RequestUri);

        uriBuilder.Query = string.IsNullOrEmpty(uriBuilder.Query) 
            ? $"subscription-key={_subscriptionKey}" 
            : $"{uriBuilder.Query}&subscription-key={_subscriptionKey}";
        
        request.RequestUri = uriBuilder.Uri;
            
        return base.SendAsync(request, cancellationToken);
    }
}