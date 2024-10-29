using ArrangeDependencies.Autofac;
using ArrangeDependencies.Autofac.Extensions;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Tradgardsgolf.Api.RequestHandling.Configuration;
using Tradgardsgolf.Contracts.Settings;

namespace Tradgardsgolf.Api.RequestHandling.Tests.Configuration;

[TestFixture]
public class AllowPlayDistance
{
    [Test]
    public async Task ShouldGetAllowPlayDistance()
    {
        var arrange = Arrange.Dependencies<QueryAllowPlayDistanceHandler, QueryAllowPlayDistanceHandler>(dependencies =>
        {
            dependencies.UseMock<IOptionsMonitor<Tradgardsgolf.Core.Config.AllowPlayDistance>>(mock => mock
                .Setup(x => x.CurrentValue).Returns(new Tradgardsgolf.Core.Config.AllowPlayDistance()
                {
                    Value = 100
                }));
        });
        
        var handler = arrange.Resolve<QueryAllowPlayDistanceHandler>();
        var result = await handler.Handle(new QueryAllowPlayDistance(), CancellationToken.None);

        result.Value.Should().Be(100);
    }
}