using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using Tradgardsgolf.Contracts.Settings;
using Tradgardsgolf.Core.Config;

namespace Tradgardsgolf.Api.RequestHandling.Configuration;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class QueryAllowPlayDistanceHandler(IOptionsMonitor<AllowPlayDistance> settings)
    : IRequestHandler<QueryAllowPlayDistance, SettingResponse<int>>
{
    public Task<SettingResponse<int>> Handle(QueryAllowPlayDistance request, CancellationToken cancellationToken)
    {
        var response = new SettingResponse<int>
        {
            Value = settings.CurrentValue.Value
        };

        return Task.FromResult(response);
    }
}