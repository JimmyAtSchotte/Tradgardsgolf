using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using Tradgardsgolf.Contracts.Settings;
using Tradgardsgolf.Core.Config;

namespace Tradgardsgolf.Api.RequestHandling.Configuration;

public class AllowPlayDistanceHandler(IOptionsMonitor<AllowPlayDistance> settings)
    : IRequestHandler<AllowPlayDistanceCommand, SettingResponse<int>>
{
    public Task<SettingResponse<int>> Handle(AllowPlayDistanceCommand request, CancellationToken cancellationToken)
    {
        var response = new SettingResponse<int>()
        {
            Value = settings.CurrentValue.Value
        };
        
        return Task.FromResult(response);
    }
}