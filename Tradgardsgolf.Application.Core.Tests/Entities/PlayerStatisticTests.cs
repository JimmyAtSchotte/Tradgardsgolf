﻿using FluentAssertions;
using Tradgardsgolf.Core.Entities;

namespace Tradgardsgolf.Application.Core.Tests.Entities;

[TestFixture]
public class PlayerStatisticTests
{
    [Test]
    public void AddScorecardSinglePlayer()
    {
        const string playerName = "Jimmy";
        
        var course = Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());
        var scorecard = Scorecard.Create(course.Id, course.GetRevision());
        scorecard.AddPlayerScores(playerName, 1, 2, 3);
        var playerStatistic = PlayerStatistic.Create(course.Id,0, playerName);

        playerStatistic.Add(scorecard);

        playerStatistic.AverageScore.Should().Be(6);
        playerStatistic.HoleStatistics.ElementAt(0).AverageScore.Should().Be(1);
        playerStatistic.HoleStatistics.ElementAt(1).AverageScore.Should().Be(2);
        playerStatistic.HoleStatistics.ElementAt(2).AverageScore.Should().Be(3);
        playerStatistic.HoleStatistics.ElementAt(0).HoleInOnes.Should().Be(1);
        playerStatistic.HoleStatistics.ElementAt(1).HoleInOnes.Should().Be(0);
        playerStatistic.HoleStatistics.ElementAt(2).HoleInOnes.Should().Be(0);
    }
    
    [Test]
    public void AddScorecardMultiplePlayers()
    {
        const string playerName1 = "Jimmy";
        const string playerName2 = "Patrik";
        
        var course = Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());
        var scorecard = Scorecard.Create(course.Id, course.GetRevision());
        scorecard.AddPlayerScores(playerName1, 1, 2, 3);
        scorecard.AddPlayerScores(playerName2, 3, 4, 5);
        var playerStatistic1 = PlayerStatistic.Create(course.Id,0, playerName1);
        var playerStatistic2 = PlayerStatistic.Create(course.Id,0, playerName2);

        playerStatistic1.Add(scorecard);
        playerStatistic2.Add(scorecard);

        playerStatistic1.AverageScore.Should().Be(6);
        playerStatistic2.AverageScore.Should().Be(12);
        playerStatistic1.HoleStatistics.ElementAt(0).AverageScore.Should().Be(1);
        playerStatistic1.HoleStatistics.ElementAt(1).AverageScore.Should().Be(2);
        playerStatistic1.HoleStatistics.ElementAt(2).AverageScore.Should().Be(3);
        playerStatistic1.HoleStatistics.ElementAt(0).HoleInOnes.Should().Be(1);
        playerStatistic1.HoleStatistics.ElementAt(1).HoleInOnes.Should().Be(0);
        playerStatistic1.HoleStatistics.ElementAt(2).HoleInOnes.Should().Be(0);
        playerStatistic2.HoleStatistics.ElementAt(0).AverageScore.Should().Be(3);
        playerStatistic2.HoleStatistics.ElementAt(1).AverageScore.Should().Be(4);
        playerStatistic2.HoleStatistics.ElementAt(2).AverageScore.Should().Be(5);
        playerStatistic2.HoleStatistics.ElementAt(0).HoleInOnes.Should().Be(0);
        playerStatistic2.HoleStatistics.ElementAt(1).HoleInOnes.Should().Be(0);
        playerStatistic2.HoleStatistics.ElementAt(2).HoleInOnes.Should().Be(0);
    }
    
    
    [Test]
    public void AddMultipleScorecardsMultiplePlayers()
    {
        const string playerName1 = "Jimmy";
        const string playerName2 = "Patrik";
        
        var course = Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());
        
        var playerStatistic1 = PlayerStatistic.Create(course.Id,0, playerName1);
        var playerStatistic2 = PlayerStatistic.Create(course.Id,0, playerName2);
        
        var scorecard1 = Scorecard.Create(course.Id, course.GetRevision());
        scorecard1.AddPlayerScores(playerName1, 1, 1, 1);
        scorecard1.AddPlayerScores(playerName2, 2, 2, 2);

        playerStatistic1.Add(scorecard1);
        playerStatistic2.Add(scorecard1);
        
        var scorecard2 = Scorecard.Create(course.Id, course.GetRevision());
        scorecard2.AddPlayerScores(playerName1, 3, 3, 3);
        scorecard2.AddPlayerScores(playerName2, 4, 4, 4);
        
        playerStatistic1.Add(scorecard2);
        playerStatistic2.Add(scorecard2);

        playerStatistic1.AverageScore.Should().Be(6);
        playerStatistic2.AverageScore.Should().Be(9);
        playerStatistic1.HoleStatistics.ElementAt(0).AverageScore.Should().Be(2);
        playerStatistic1.HoleStatistics.ElementAt(1).AverageScore.Should().Be(2);
        playerStatistic1.HoleStatistics.ElementAt(2).AverageScore.Should().Be(2);
        playerStatistic2.HoleStatistics.ElementAt(0).AverageScore.Should().Be(3);
        playerStatistic2.HoleStatistics.ElementAt(1).AverageScore.Should().Be(3);
        playerStatistic2.HoleStatistics.ElementAt(2).AverageScore.Should().Be(3);
        playerStatistic1.BestScore.Score.Should().Be(3);
        playerStatistic1.BestScore.Date.Should().Be(scorecard1.Date);
    }
    
    [Test]
    public void PlayerNotInScorecard()
    {
        const string playerName = "Jimmy";
        
        var course = Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());
        
        var playerStatistic = PlayerStatistic.Create(course.Id,0, playerName);
        
        var scorecard = Scorecard.Create(course.Id, course.GetRevision());
        scorecard.Date = new DateTime(2020, 01, 01);
        scorecard.AddPlayerScores("Random player", 1, 1, 1);
        playerStatistic.Add(scorecard);

        playerStatistic.AverageScore.Should().Be(0);
        playerStatistic.TimesPlayed.Should().Be(0);
        playerStatistic.HoleStatistics.Should().BeEmpty();
        playerStatistic.BestScore.Score.Should().Be(0);
        playerStatistic.BestScore.Date.Should().Be(DateTime.MinValue);
    }
    
    [Test]
    public void NotCorrectCourseRevision()
    {
        const string playerName = "Jimmy";
        
        var course = Course.Create(Guid.NewGuid(), p =>
        {
            p.Id = Guid.NewGuid();
            p.Revision = 1;
        });
        
        var playerStatistic = PlayerStatistic.Create(course.Id, 0, playerName);
        
        var scorecard = Scorecard.Create(course.Id, course.GetRevision());
        scorecard.Date = new DateTime(2020, 01, 01);
        scorecard.AddPlayerScores(playerName, 1, 1, 1);
        
        playerStatistic.Add(scorecard);

        playerStatistic.AverageScore.Should().Be(0);
        playerStatistic.TimesPlayed.Should().Be(0);
        playerStatistic.HoleStatistics.Should().BeEmpty();
        playerStatistic.BestScore.Score.Should().Be(0);
        playerStatistic.BestScore.Date.Should().Be(DateTime.MinValue);
    }
}