using ArrangeDependencies.Autofac;
using ArrangeDependencies.Autofac.Extensions;
using FluentAssertions;
using Moq;
using Tradgardsgolf.Api.RequestHandling.Course;
using Tradgardsgolf.Contracts.Statistics;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;
using Tradgardsgolf.Core.Specifications.Scorecard;

namespace Tradgardsgolf.Api.RequestHandling.Tests.Course;

[TestFixture]
public class QueryCourseStatistics
{
    [Test]
    public async Task ShouldFindScorecardsOnCourse()
    {
        var course = Core.Entities.Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());
        var scorecards = new List<Core.Entities.Scorecard>();
        scorecards.Add(course.CreateScorecard());
        
        var arrange = Arrange.Dependencies<QueryCourseStatisticsHandler, QueryCourseStatisticsHandler>(dependencies =>
        {
            dependencies.UseMock<IRepository>(mock =>
            {
                mock.Setup(x => x.ListAsync(Specs.Scorecard.ByCourse(course.Id), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(scorecards);
            });
        });
        
        var handler = arrange.Resolve<QueryCourseStatisticsHandler>();
        var command = new Contracts.Statistics.QueryCourseStatistics()
        {
            CourseId = course.Id
        };
        var result = await handler.Handle(command, CancellationToken.None);

        result.Scorecards.Should().HaveCount(1);
    }
    
    
    [Test]
    public async Task ShouldTransformScores()
    {
        var course = Core.Entities.Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());
        var scorecards = new List<Core.Entities.Scorecard>();
        var scorecard1 = course.CreateScorecard();
        
        scorecard1.AddPlayerScores("TestA", 3, 5, 2);
        scorecard1.AddPlayerScores("TestB", 2, 2, 2);
        scorecard1.AddPlayerScores("TestC", 4, 4, 3);
        
        scorecards.Add(scorecard1);
        
        var arrange = Arrange.Dependencies<QueryCourseStatisticsHandler, QueryCourseStatisticsHandler>(dependencies =>
        {
            dependencies.UseMock<IRepository>(mock =>
            {
                mock.Setup(x => x.ListAsync(Specs.Scorecard.ByCourse(course.Id), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(scorecards);
            });
        });
        
        var handler = arrange.Resolve<QueryCourseStatisticsHandler>();
        var command = new Contracts.Statistics.QueryCourseStatistics()
        {
            CourseId = course.Id
        };
        var result = await handler.Handle(command, CancellationToken.None);

        result.Scorecards.First().Scores.Where(x => x.Player == "TestA").Should().HaveCount(3);
        result.Scorecards.First().Scores.Where(x => x.Player == "TestA").ElementAt(0).Score.Should().Be(3);
        result.Scorecards.First().Scores.Where(x => x.Player == "TestA").ElementAt(1).Score.Should().Be(5);
        result.Scorecards.First().Scores.Where(x => x.Player == "TestA").ElementAt(2).Score.Should().Be(2);
        result.Scorecards.First().Scores.Where(x => x.Player == "TestA").ElementAt(0).Hole.Should().Be(1);
        result.Scorecards.First().Scores.Where(x => x.Player == "TestA").ElementAt(1).Hole.Should().Be(2);
        result.Scorecards.First().Scores.Where(x => x.Player == "TestA").ElementAt(2).Hole.Should().Be(3);
    }
}