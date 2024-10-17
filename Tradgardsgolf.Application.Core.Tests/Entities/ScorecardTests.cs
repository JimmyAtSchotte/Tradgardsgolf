using FluentAssertions;
using Tradgardsgolf.Core.Entities;

namespace Tradgardsgolf.Application.Core.Tests.Entities;

[TestFixture]
public class ScorecardTests
{
    [Test]
    public void ShouldCreateScorecardOnCourse()
    {
        var course = Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());
        var scorecard = course.CreateScorecard();
        course.Scorecards.Should().HaveCount(1);
        scorecard.Course.Should().Be(course);
        scorecard.CourseId.Should().Be(course.Id);
        scorecard.Date.Should().NotBe(DateTime.MinValue);
    }

    [Test]
    public void ShouldAddPlayerScores()
    {
        var course = Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());
        var scorecard = course.CreateScorecard();
        
        scorecard.AddPlayerScores("Test", 1, 2, 3);
        
        scorecard.Scores.Should().HaveCount(1);
        scorecard.Scores["Test"].Should().HaveCount(3);
    }
}