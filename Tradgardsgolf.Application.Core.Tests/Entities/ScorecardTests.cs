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
        var scorecard = Scorecard.Create(course.Id, course.GetRevision());
        
        scorecard.CourseId.Should().Be(course.Id);
        scorecard.Date.Should().NotBe(DateTime.MinValue);
    }

    [Test]
    public void ShouldAddPlayerScores()
    {
        var course = Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());
        var scorecard = Scorecard.Create(course.Id, course.GetRevision());
        
        scorecard.AddPlayerScores("Test", 1, 2, 3);
        
        scorecard.Scores.Should().HaveCount(1);
        scorecard.Scores["Test"].Should().HaveCount(3);
    }
    
    [Test]
    public void ShouldReplaceName()
    {
        var course = Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());
        var scorecard = Scorecard.Create(course.Id, course.GetRevision());
        
        scorecard.AddPlayerScores("Test", 1, 2, 3);
        
        scorecard.ReplaceName("Test", "Updated");
        
        scorecard.Scores["Updated"].Should().HaveCount(3);
        scorecard.Scores.ContainsKey("Test").Should().BeFalse();
    }
    
    [Test]
    public void ShouldNotThrowWhenReplaceNameDoesntExists()
    {
        var course = Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());
        var scorecard = Scorecard.Create(course.Id, course.GetRevision());
        
        scorecard.AddPlayerScores("Test", 1, 2, 3);
        
        scorecard.Invoking(x => x.ReplaceName("Test1", "Updated")).Should().NotThrow();
        
    }
}