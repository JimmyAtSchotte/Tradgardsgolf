using ArrangeDependencies.Autofac;
using ArrangeDependencies.Autofac.Extensions;
using FluentAssertions;
using Moq;
using Tradgardsgolf.Api.RequestHandling.Scorecard;
using Tradgardsgolf.Contracts.Scorecard;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;
using Tradgardsgolf.Core.Specifications.Course;

namespace Tradgardsgolf.Api.RequestHandling.Tests.Scorecard;

[TestFixture]
public class SaveScorecard
{
    [Test]
    public async Task ShouldSaveScorecard()
    {
        var course = Core.Entities.Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());
        var updatedCourses = new List<Core.Entities.Course>();
        
        var arrange = Arrange.Dependencies<SaveScorecardHandler, SaveScorecardHandler>(dependencies =>
        {
            dependencies.UseMock<IRepository<Core.Entities.Course>>(mock =>
            {
                mock.Setup(x => x.FirstOrDefaultAsync(Specs.Course.ById(course.Id), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(course);
                
                mock.Setup(x => x.UpdateAsync(It.IsAny<Core.Entities.Course>(), It.IsAny<CancellationToken>()))
                    .Callback((Core.Entities.Course c, CancellationToken t) => updatedCourses.Add(c));
            });
        });
        
        var handler = arrange.Resolve<SaveScorecardHandler>();
        var scores = new List<int>() { 2, 3, 5, 2, 4, 1 };
        var command = new SaveScorecardCommand()
        {
            CourseId = course.Id,
            PlayerScores = new List<PlayerScore>()
            {
                new PlayerScore()
                {
                    Name = "Player A",
                    HoleScores = scores
                }
            }
        };
        
        var result = await handler.Handle(command, CancellationToken.None);

        updatedCourses.Should().HaveCount(1);
        updatedCourses.First().Scorecards.Should().HaveCount(1);
        updatedCourses.First().Scorecards.First().Scores.Should().ContainKey("Player A");
        updatedCourses.First().Scorecards.First().Scores["Player A"].Should().BeEquivalentTo(scores);

        result.PlayerScores.Should().BeEquivalentTo(command.PlayerScores);
        result.CourseId.Should().Be(course.Id);
        result.Id.Should().Be(updatedCourses.First().Scorecards.First().Id);
    }
}