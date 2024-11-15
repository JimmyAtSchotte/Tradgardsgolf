using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Tradgardsgolf.Api.RequestHandling.Health;

[SuppressMessage("ReSharper", "UnusedType.Global")]
public class IsHealthyHandler : IRequestHandler<Contracts.Health.IsHealthy, Unit>
{
    public Task<Unit> Handle(Contracts.Health.IsHealthy request, CancellationToken cancellationToken)
    {
        return Task.FromResult(Unit.Value);
    }
}
