using FluentAssertions;
using Tradgardsgolf.Core.Entities;

namespace Tradgardsgolf.Application.Core.Tests.Entities;

[TestFixture]
public class CourseSeasonTests
{
    [Test]
    public void AddScorecardSinglePlayer()
    {
        const string playerName = "Jimmy";
        
        var course = Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());
        var scorecard = Scorecard.Create(course.Id, course.Revision);
        scorecard.AddPlayerScores(playerName, 1, 2, 3);
        var courseSeason = CourseSeason.Create(course.Id, "2024");

        courseSeason.Add(scorecard);
        
        courseSeason.Players.ContainsKey(playerName).Should().BeTrue();
        courseSeason.Players[playerName].ElementAt(0).Should().Be(6);
    }
    
    [Test]
    public void AddScorecardMultiplePlayers()
    {
        
        var course = Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());
        var scorecard = Scorecard.Create(course.Id, course.Revision);
        scorecard.AddPlayerScores("test a", 1, 2, 3);
        scorecard.AddPlayerScores("test b", 1, 2, 3);
        var courseSeason = CourseSeason.Create(course.Id, "2024");

        courseSeason.Add(scorecard);
        
        courseSeason.Players.Count.Should().Be(2);
    }
    
    [Test]
    public void AddMultipleScorecardsSamePlayer()
    {
        const string playerName = "Jimmy";
        
        var course = Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());

        var courseSeason = CourseSeason.Create(course.Id, "2024");
        
        var scorecard1 =Scorecard.Create(course.Id, course.Revision);
        scorecard1.AddPlayerScores(playerName, 1, 2, 3);
        courseSeason.Add(scorecard1);
        
        var scorecard2 = Scorecard.Create(course.Id, course.Revision);
        scorecard2.AddPlayerScores(playerName, 1, 2, 3);
        courseSeason.Add(scorecard2);
        
        var scorecard3 = Scorecard.Create(course.Id, course.Revision);
        scorecard3.AddPlayerScores(playerName, 1, 2, 3);
        courseSeason.Add(scorecard3);

        courseSeason.Players[playerName].Should().HaveCount(3);
    }
}