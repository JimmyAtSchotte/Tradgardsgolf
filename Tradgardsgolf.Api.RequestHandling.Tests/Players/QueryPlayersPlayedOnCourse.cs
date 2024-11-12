using ArrangeDependencies.Autofac;
using ArrangeDependencies.Autofac.Extensions;
using FluentAssertions;
using Moq;
using Tradgardsgolf.Api.RequestHandling.Player;
using Tradgardsgolf.Contracts.Players;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;
using Tradgardsgolf.Core.Specifications.Scorecard;

namespace Tradgardsgolf.Api.RequestHandling.Tests.Players;

[TestFixture]
public class QueryPlayersPlayedOnCourse
{
    [Test]
    public async Task ShouldHaveNoPlayers()
    {
        var course = Core.Entities.Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());

        var arrange = Arrange.Dependencies<QueryPlayersPlayedOnCourseHandler, QueryPlayersPlayedOnCourseHandler>(dependencies =>
        {
            dependencies.UseMock<IRepository>(mock =>
            {
                mock.Setup(x => x.ListAsync(Specs.Scorecard.ByCourse(course.Id), It.IsAny<CancellationToken>()))
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
        var course = Core.Entities.Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());

        var scorecard = Core.Entities.Scorecard.Create(course.Id, course.Revision);
        scorecard.AddPlayerScores("B", 2);
        scorecard.AddPlayerScores("A", 1);

        var arrange = Arrange.Dependencies<QueryPlayersPlayedOnCourseHandler, QueryPlayersPlayedOnCourseHandler>(dependencies =>
        {
            dependencies.UseMock<IRepository>(mock =>
            {
                mock.Setup(x => x.ListAsync(Specs.Scorecard.ByCourse(course.Id), It.IsAny<CancellationToken>()))
                    .ReturnsAsync([scorecard]);
            });
        });
        
        var handler = arrange.Resolve<QueryPlayersPlayedOnCourseHandler>();
        var command = new Contracts.Players.QueryPlayersPlayedOnCourse()
        {
            CourseId = course.Id,
        };
        
        var result = await handler.Handle(command, CancellationToken.None);

        result.ElementAt(0).Name.Should().Be("A");
        result.ElementAt(1).Name.Should().Be("B");
    }
    
    [Test]
    public async Task ShouldOrderPlayersOnHowMuchTheyHavePlayedOnCourse()
    {
        var course = Core.Entities.Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());
        var scorecards = new List<Core.Entities.Scorecard>();

        for (int i = 0; i < 100; i++)
        {
            var scorecard = Core.Entities.Scorecard.Create(course.Id, course.Revision);
            scorecard.AddPlayerScores("MoreThan50-B", 2);
            scorecards.Add(scorecard);
        }

        for (int i = 0; i < 11; i++)
        {
            var scorecard = Core.Entities.Scorecard.Create(course.Id, course.Revision);
            scorecard.AddPlayerScores("MoreThan10-A", 2);
            scorecards.Add(scorecard);
        }

        for (int i = 0; i < 50; i++)
        {
            var scorecard = Core.Entities.Scorecard.Create(course.Id, course.Revision);
            scorecard.AddPlayerScores("MoreThan10-B", 2);
            scorecards.Add(scorecard);
        }

        for (int i = 0; i < 3; i++)
        {
            var scorecard = Core.Entities.Scorecard.Create(course.Id, course.Revision);
            scorecard.AddPlayerScores("LessThan10-A", 2);
            scorecards.Add(scorecard);
        }

        for (int i = 0; i < 10; i++)
        {
            var scorecard = Core.Entities.Scorecard.Create(course.Id, course.Revision);
            scorecard.AddPlayerScores("LessThan10-B", 2);
            scorecards.Add(scorecard);
        }

        for (int i = 0; i < 51; i++)
        {
            var scorecard = Core.Entities.Scorecard.Create(course.Id, course.Revision);
            scorecard.AddPlayerScores("MoreThan50-A", 2);
            scorecards.Add(scorecard);
        }

        var arrange = Arrange.Dependencies<QueryPlayersPlayedOnCourseHandler, QueryPlayersPlayedOnCourseHandler>(dependencies =>
        {
            dependencies.UseMock<IRepository>(mock =>
            {
                mock.Setup(x => x.ListAsync(Specs.Scorecard.ByCourse(course.Id), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(scorecards);
            });
        });
        
        var handler = arrange.Resolve<QueryPlayersPlayedOnCourseHandler>();
        var command = new Contracts.Players.QueryPlayersPlayedOnCourse()
        {
            CourseId = course.Id,
        };
        
        var result = await handler.Handle(command, CancellationToken.None);

        result.ElementAt(0).Name.Should().Be("MoreThan50-A");
        result.ElementAt(1).Name.Should().Be("MoreThan50-B");
        result.ElementAt(2).Name.Should().Be("MoreThan10-A");
        result.ElementAt(3).Name.Should().Be("MoreThan10-B");
        result.ElementAt(4).Name.Should().Be("LessThan10-A");
        result.ElementAt(5).Name.Should().Be("LessThan10-B");
    }
}