using FluentAssertions;
using Tradgardsgolf.Core.Entities;
using Tradgardsgolf.Core.Services;

namespace Tradgardsgolf.Application.Core.Tests.Services;

public class CourseStatisticsServiceTests
{
    [Test]
    public void CourseHasNoRevision()
    {
        var course = Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());
        var scorecard = Scorecard.Create(course.Id, course.Revision);

        var courseMigrator = new CourseStatisticService(course, [scorecard]);

        courseMigrator.ShouldMigrateCourseToRevision().Should().BeFalse();
    }
    
    
    [Test]
    public void CourseHasScoreReset()
    {
        var course = Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());
        course.ScoreReset = DateTime.Today;
        
        var scorecard = Scorecard.Create(course.Id, course.Revision);

        var courseMigrator = new CourseStatisticService(course, [scorecard]);

        courseMigrator.ShouldMigrateCourseToRevision().Should().BeTrue();
    }
    
    [Test]
    public void CourseHasScoreResetAndRevision()
    {
        var course = Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());
        course.ScoreReset = DateTime.Today;
        course.Revision = 1;
        
        var scorecard = Scorecard.Create(course.Id, course.Revision);

        var courseMigrator = new CourseStatisticService(course, [scorecard]);

        courseMigrator.ShouldMigrateCourseToRevision().Should().BeFalse();
    }
    
    
    [Test]
    public void MigrateCourseToRevision()
    {
        var course = Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());
        course.ScoreReset = DateTime.Today;
        
        var scorecard = Scorecard.Create(course.Id, course.Revision);

        var courseMigrator = new CourseStatisticService(course, [scorecard]);

        var result = courseMigrator.MigrateCourseToRevision();
        
        result.Revision.Should().Be(1);
        result.ScoreReset.Should().Be(course.ScoreReset);
    }
    
    [Test]
    public void MigrateScorecardsToRevision()
    {
        var course = Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());
        course.ScoreReset = DateTime.Now.AddDays(-10);
        course.Revision = 1;
        
        var scorecard = Scorecard.Create(course.Id, 0);

        var courseMigrator = new CourseStatisticService(course, [scorecard]);

        var result = courseMigrator.MigrateScorecardsToRevision();

        result.Should().HaveCount(1);
        scorecard.CourseRevision.Should().Be(1);
    }
    
    
    [Test]
    public void ShouldIgnoreMigratedScorecards()
    {
        var course = Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());
        course.ScoreReset = DateTime.Now.AddDays(-10);
        course.Revision = 1;
        
        var scorecard = Scorecard.Create(course.Id, 1);

        var courseMigrator = new CourseStatisticService(course, [scorecard]);

        var result = courseMigrator.MigrateScorecardsToRevision();

        result.Should().HaveCount(0);
        scorecard.CourseRevision.Should().Be(1);
    }
    
    [Test]
    public void ShouldIgnoreScorecardsCreatedBeforeScoreReset()
    {
        var course = Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());
        course.ScoreReset = DateTime.Now.AddDays(-10);
        course.Revision = 1;
        
        var scorecard = Scorecard.Create(course.Id, 0);
        scorecard.Date = course.ScoreReset.Value.AddDays(-1);

        var courseMigrator = new CourseStatisticService(course, [scorecard]);

        var result = courseMigrator.MigrateScorecardsToRevision();

        result.Should().HaveCount(0);
        scorecard.CourseRevision.Should().Be(0);
    }
    
    [Test]
    public void ShouldIgnoreScorecardsOnCourseWithNoScoreReset()
    {
        var course = Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());
  
        var scorecard = Scorecard.Create(course.Id, 0);

        var courseMigrator = new CourseStatisticService(course, [scorecard]);

        var result = courseMigrator.MigrateScorecardsToRevision();

        result.Should().HaveCount(0);
        scorecard.CourseRevision.Should().Be(0);
    }
    
    [Test]
    public void MigratePlayerStats()
    {
        var course = Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());
        var scorecard = Scorecard.Create(course.Id, 0);
        scorecard.AddPlayerScores("Player1", 1,1,1);

        var courseMigrator = new CourseStatisticService(course, [scorecard]);

        var result = courseMigrator.GeneratePlayerStatistics().ToList();

        result.Should().HaveCount(1);
        result.First().Name.Should().Be("Player1");
        result.First().BestScore.Score.Should().Be(3);
        result.First().AverageScore.Should().Be(3);
    }
    
    
    [Test]
    public void ResetPlayerStats()
    {
        var course = Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());
        var scorecard = Scorecard.Create(course.Id, 0);
        scorecard.AddPlayerScores("Player1", 1,1,1);
        
        var playerStatistic = PlayerStatistic.Create(scorecard.CourseId, scorecard.CourseRevision, "Player1");
        playerStatistic.Add(scorecard);
        
        var courseMigrator = new CourseStatisticService(course, [scorecard], [playerStatistic], []);

        var result = courseMigrator.GeneratePlayerStatistics().ToList();

        result.Should().HaveCount(1);
        result.First().Name.Should().Be("Player1");
        result.First().BestScore.Score.Should().Be(3);
        result.First().AverageScore.Should().Be(3);
        result.First().TimesPlayed.Should().Be(1);
    }
    
    
    [Test]
    public void MigrateCourseSeasons()
    {
        var course = Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());
        var scorecard = Scorecard.Create(course.Id, 0);
        scorecard.AddPlayerScores("Player1", 1,1,1);
        
        var scorecard2 = Scorecard.Create(course.Id, 0);
        scorecard2.AddPlayerScores("Player1", 1,1,1);

        var courseMigrator = new CourseStatisticService(course, [scorecard, scorecard2]);

        var result = courseMigrator.GenerateCourseSeasons().ToList();

        result.Should().HaveCount(1);
        result.First().Players.Should().HaveCount(1);
        result.First().Players.ContainsKey("Player1").Should().BeTrue();
        result.First().Season.Should().Be(scorecard.GetSeason());
    }
    
        
    [Test]
    public void ResetCourseSeasons()
    {
        var course = Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());
        var scorecard = Scorecard.Create(course.Id, 0);
        scorecard.AddPlayerScores("Player1", 1,1,1);
        
        var scorecard2 = Scorecard.Create(course.Id, 0);
        scorecard2.AddPlayerScores("Player1", 1,1,1);

        var courseSeason = CourseSeason.Create(scorecard.CourseId, scorecard.GetSeason());
        courseSeason.Id = Guid.NewGuid();
        courseSeason.Add(scorecard);
        courseSeason.Add(scorecard2);
        
        var courseMigrator = new CourseStatisticService(course, [scorecard, scorecard2], [], [courseSeason]);

        var result = courseMigrator.GenerateCourseSeasons().ToList();

        result.Should().HaveCount(1);
        result.First().Id.Should().Be(courseSeason.Id);
        result.First().Players.Should().HaveCount(1);
        result.First().Players.ContainsKey("Player1").Should().BeTrue();
        result.First().Players["Player1"].Should().HaveCount(2);
        result.First().Season.Should().Be(scorecard.GetSeason());
    }
}