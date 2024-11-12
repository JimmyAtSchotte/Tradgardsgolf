using ArrangeDependencies.Autofac;
using ArrangeDependencies.Autofac.Extensions;
using FluentAssertions;
using Moq;
using Tradgardsgolf.Api.RequestHandling.Player;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;
using Tradgardsgolf.Core.Specifications.PlayerStatistic;

namespace Tradgardsgolf.Api.RequestHandling.Tests.Players;

[TestFixture]
public class QueryPlayersPlayedOnCourse
{
    private readonly Guid courseId = Guid.NewGuid();
    
    
    [Test]
    public async Task ShouldHaveNoPlayers()
    {
        var course = Core.Entities.Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());

        var arrange = Arrange.Dependencies<QueryPlayersPlayedOnCourseHandler, QueryPlayersPlayedOnCourseHandler>(dependencies =>
        {
            dependencies.UseMock<IRepository>(mock =>
            {
                mock.Setup(x => x.ListAsync(Specs.PlayerStatistic.ByCourse(courseId), It.IsAny<CancellationToken>()))
                    .ReturnsAsync([]);
            });
        });
        
        var handler = arrange.Resolve<QueryPlayersPlayedOnCourseHandler>();
        var command = new Contracts.Players.QueryPlayersPlayedOnCourse()
        {
            CourseId = course.Id,
        };
        
        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().BeEmpty();
    }
    
    [Test]
    public async Task ShouldOrderPlayersByName()
    {
        var playerStatistics = new Core.Entities.PlayerStatistic[]
        {
            CreatePlayerStatistic("B", 3),
            CreatePlayerStatistic("A", 3),
            
        };

        var arrange = Arrange.Dependencies<QueryPlayersPlayedOnCourseHandler, QueryPlayersPlayedOnCourseHandler>(dependencies =>
        {
            dependencies.UseMock<IRepository>(mock =>
            {
                mock.Setup(x => x.ListAsync(Specs.PlayerStatistic.ByCourse(courseId), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(playerStatistics);
            });
        });
        
        var handler = arrange.Resolve<QueryPlayersPlayedOnCourseHandler>();
        var command = new Contracts.Players.QueryPlayersPlayedOnCourse()
        {
            CourseId = courseId,
        };
        
        var result = await handler.Handle(command, CancellationToken.None);

        result.ElementAt(0).Name.Should().Be("A");
        result.ElementAt(1).Name.Should().Be("B");
    }
    
    [Test]
    public async Task ShouldOrderPlayersOnHowMuchTheyHavePlayedOnCourse()
    {
        var playerStatistics = new Core.Entities.PlayerStatistic[]
        {
            CreatePlayerStatistic("MoreThan50-B", 100),
            CreatePlayerStatistic("MoreThan50-A", 51),
            CreatePlayerStatistic("MoreThan10-A", 11),
            CreatePlayerStatistic("MoreThan10-B", 50),
            CreatePlayerStatistic("LessThan10-A", 3),
            CreatePlayerStatistic("LessThan10-B", 9),
            
        };

        var arrange = Arrange.Dependencies<QueryPlayersPlayedOnCourseHandler, QueryPlayersPlayedOnCourseHandler>(dependencies =>
        {
            dependencies.UseMock<IRepository>(mock =>
            {
                mock.Setup(x => x.ListAsync(Specs.PlayerStatistic.ByCourse(courseId), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(playerStatistics);
            });
        });
        
        var handler = arrange.Resolve<QueryPlayersPlayedOnCourseHandler>();
        var command = new Contracts.Players.QueryPlayersPlayedOnCourse()
        {
            CourseId = courseId,
        };
        
        var result = await handler.Handle(command, CancellationToken.None);

        result.ElementAt(0).Name.Should().Be("MoreThan50-A");
        result.ElementAt(1).Name.Should().Be("MoreThan50-B");
        result.ElementAt(2).Name.Should().Be("MoreThan10-A");
        result.ElementAt(3).Name.Should().Be("MoreThan10-B");
        result.ElementAt(4).Name.Should().Be("LessThan10-A");
        result.ElementAt(5).Name.Should().Be("LessThan10-B");
    }
    
    [Test]
    public async Task ShouldOrderPlayersOnHowMuchTheyHavePlayedOnCourseNoMatterRevisions()
    {
        var playerStatistics = new Core.Entities.PlayerStatistic[]
        {
            CreatePlayerStatistic("A", 40),
            CreatePlayerStatistic("B", 30, 0),
            CreatePlayerStatistic("B", 30, 1),
            
        };

        var arrange = Arrange.Dependencies<QueryPlayersPlayedOnCourseHandler, QueryPlayersPlayedOnCourseHandler>(dependencies =>
        {
            dependencies.UseMock<IRepository>(mock =>
            {
                mock.Setup(x => x.ListAsync(Specs.PlayerStatistic.ByCourse(courseId), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(playerStatistics);
            });
        });
        
        var handler = arrange.Resolve<QueryPlayersPlayedOnCourseHandler>();
        var command = new Contracts.Players.QueryPlayersPlayedOnCourse()
        {
            CourseId = courseId,
        };
        
        var result = await handler.Handle(command, CancellationToken.None);

        result.ElementAt(0).Name.Should().Be("B");
        result.ElementAt(1).Name.Should().Be("A");
    }

    private Core.Entities.PlayerStatistic CreatePlayerStatistic(string name, int timesPlayed, int courseRevision = 0)
    {
        var playerStatistic = Core.Entities.PlayerStatistic.Create(courseId, courseRevision, name);
        playerStatistic.TimesPlayed = timesPlayed;
        return playerStatistic;
    }
}