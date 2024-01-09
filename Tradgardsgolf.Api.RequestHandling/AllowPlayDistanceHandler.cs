using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using Tradgardsgolf.Contracts.Settings;
using Tradgardsgolf.Core.Config;

namespace Tradgardsgolf.Api.RequestHandling;

public class AllowPlayDistanceHandler(IOptions<Settings> settings)
    : IRequestHandler<AllowPlayDistanceCommand, SettingResponse<int>>
{
    public Task<SettingResponse<int>> Handle(AllowPlayDistanceCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new SettingResponse<int>()
        {
            Value = settings.Value.AllowPlayDistance
        });
    }
}