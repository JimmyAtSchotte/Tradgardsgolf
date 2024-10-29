using ArrangeDependencies.Autofac;
using ArrangeDependencies.Autofac.Extensions;
using FluentAssertions;
using Moq;
using Tradgardsgolf.Api.RequestHandling.Tournament;
using Tradgardsgolf.Contracts.Tournament;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;
using Tradgardsgolf.Core.Specifications.Tournament;

namespace Tradgardsgolf.Api.RequestHandling.Tests.Tournament;

[TestFixture]
public class ListTodaysTournamets
{
    [Test]
    public async Task ShouldNotHaveAnyTournaments()
    {
        var course = Core.Entities.Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());
        

        var arrange = Arrange.Dependencies<ListTodaysTournamentsHandler, ListTodaysTournamentsHandler>(dependencies =>
        {
            dependencies.UseMock<IRepository<Core.Entities.Tournament>>(mock =>
            {
                mock.Setup(x => x.ListAsync(Specs.Tournament.ByCourseAndDate(course.Id, DateTime.Today), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(new List<Core.Entities.Tournament>());
            });
        });
        
        var handler = arrange.Resolve<ListTodaysTournamentsHandler>();
        var command = new ListTodaysTournamentsCommand()
        {
            CourseId = course.Id,
        };

        var result = await handler.Handle(command, CancellationToken.None);
        result.Should().BeEmpty();

    }
    
    [Test]
    public async Task ShouldHaveTournaments()
    {
        var course = Core.Entities.Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());
        var tournament = Core.Entities.Tournament.Create("tournament");
        tournament.Id = Guid.NewGuid();
        
        var arrange = Arrange.Dependencies<ListTodaysTournamentsHandler, ListTodaysTournamentsHandler>(dependencies =>
        {
            dependencies.UseMock<IRepository<Core.Entities.Tournament>>(mock =>
            {
                mock.Setup(x => x.ListAsync(Specs.Tournament.ByCourseAndDate(course.Id, DateTime.Today), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(new List<Core.Entities.Tournament>()
                    {
                        tournament
                    });
            });
        });
        
        var handler = arrange.Resolve<ListTodaysTournamentsHandler>();
        var command = new ListTodaysTournamentsCommand()
        {
            CourseId = course.Id,
        };

        var result = await handler.Handle(command, CancellationToken.None);
        result.Should().HaveCount(1);
        result.First().Name.Should().Be(tournament.Name);
        result.First().Id.Should().Be(tournament.Id);

    }
}