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
        var arrange = Arrange.Dependencies<AllowPlayDistanceHandler, AllowPlayDistanceHandler>(dependencies =>
        {
            dependencies.UseMock<IOptionsMonitor<Tradgardsgolf.Core.Config.AllowPlayDistance>>(mock => mock
                .Setup(x => x.CurrentValue).Returns(new Tradgardsgolf.Core.Config.AllowPlayDistance()
                {
                    Value = 100
                }));
        });
        
        var handler = arrange.Resolve<AllowPlayDistanceHandler>();
        var result = await handler.Handle(new AllowPlayDistanceCommand(), CancellationToken.None);

        result.Value.Should().Be(100);
    }
}