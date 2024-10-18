using ArrangeDependencies.Autofac;
using ArrangeDependencies.Autofac.Extensions;
using FluentAssertions;
using Moq;
using Tradgardsgolf.Api.RequestHandling.Player;
using Tradgardsgolf.Contracts.Players;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications.Scorecard;

namespace Tradgardsgolf.Api.RequestHandling.Tests.Players;

[TestFixture]
public class HasPlayedOnCourse
{
    [Test]
    public async Task ShouldHaveNoPlayers()
    {
        var course = Core.Entities.Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());

        var arrange = Arrange.Dependencies<HasPlayedOnCourseHandler, HasPlayedOnCourseHandler>(dependencies =>
        {
            dependencies.UseMock<IRepository<Core.Entities.Scorecard>>(mock =>
            {
                mock.Setup(x => x.ListAsync(new ByCourse(course.Id), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(course.Scorecards.ToList);
            });
        });
        
        var handler = arrange.Resolve<HasPlayedOnCourseHandler>();
        var command = new HasPlayedOnCourseCommand()
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

        var scorecard = course.CreateScorecard();
        scorecard.AddPlayerScores("B", 2);
        scorecard.AddPlayerScores("A", 1);

        var arrange = Arrange.Dependencies<HasPlayedOnCourseHandler, HasPlayedOnCourseHandler>(dependencies =>
        {
            dependencies.UseMock<IRepository<Core.Entities.Scorecard>>(mock =>
            {
                mock.Setup(x => x.ListAsync(new ByCourse(course.Id), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(course.Scorecards.ToList);
            });
        });
        
        var handler = arrange.Resolve<HasPlayedOnCourseHandler>();
        var command = new HasPlayedOnCourseCommand()
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

        course.CreateScorecard().AddPlayerScores("A", 2);
        
        for (int i = 0; i < 11; i++)
            course.CreateScorecard().AddPlayerScores("D", 2);
        
        for (int i = 0; i < 5; i++)
            course.CreateScorecard().AddPlayerScores("B", 2);
        
        for (int i = 0; i < 51; i++)
            course.CreateScorecard().AddPlayerScores("E", 2);

        var arrange = Arrange.Dependencies<HasPlayedOnCourseHandler, HasPlayedOnCourseHandler>(dependencies =>
        {
            dependencies.UseMock<IRepository<Core.Entities.Scorecard>>(mock =>
            {
                mock.Setup(x => x.ListAsync(new ByCourse(course.Id), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(course.Scorecards.ToList);
            });
        });
        
        var handler = arrange.Resolve<HasPlayedOnCourseHandler>();
        var command = new HasPlayedOnCourseCommand()
        {
            CourseId = course.Id,
        };
        
        var result = await handler.Handle(command, CancellationToken.None);

        result.ElementAt(0).Name.Should().Be("E");
        result.ElementAt(1).Name.Should().Be("D");
        result.ElementAt(2).Name.Should().Be("A");
        result.ElementAt(3).Name.Should().Be("B");
    }
}