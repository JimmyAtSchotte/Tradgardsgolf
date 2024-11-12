using System.Collections.Generic;
using System.Linq;
using Tradgardsgolf.Core.Entities;

public class CourseStatisticService
{
    private readonly Course _course;
    private readonly IEnumerable<Scorecard> _scorecards;
    private readonly List<PlayerStatistic> _playerStatistics;
    private readonly List<CourseSeason> _courseSeasons;

    public CourseStatisticService(Course course, IEnumerable<Scorecard> scorecards, List<PlayerStatistic> playerStatistics, List<CourseSeason> courseSeasons)
    {
        _course = course;
        _scorecards = scorecards;
        _playerStatistics = playerStatistics;
        _courseSeasons = courseSeasons;
    }
    
    public CourseStatisticService(Course course, IEnumerable<Scorecard> scorecards) : this(course, scorecards, [], [])
    {
        
    }

    public bool ShouldMigrateCourseToRevision()
    {
        return _course.ScoreReset.HasValue && _course.Revision == 0;
    }

    public Course MigrateCourseToRevision()
    {
        _course.ResetScore(_course.ScoreReset.Value);
        return _course;
    }

    public IEnumerable<Scorecard> MigrateScorecardsToRevision()
    {
        if(!_course.ScoreReset.HasValue)
            yield break;
        
        foreach (var scorecard in _scorecards)
        {
            if(scorecard.CourseRevision != 0)
                continue;

            if (scorecard.Date < _course.ScoreReset.Value)
                continue;
            
            scorecard.CourseRevision = 1;
            yield return scorecard;
        }
    }

    public IEnumerable<PlayerStatistic> GeneratePlayerStatistics()
    {
        foreach (var playerStatistic in _playerStatistics)
            playerStatistic.Reset();
        
        foreach (var scorecard in _scorecards)
        {
            foreach (var playerName in scorecard.Scores.Keys)
            {
                var playerStatistic = FindOrCreatePlayerStatistic(playerName, scorecard);
                playerStatistic.Add(scorecard);
            }
        }
        
        return _playerStatistics;
    }

    private PlayerStatistic FindOrCreatePlayerStatistic(string playerName, Scorecard scorecard)
    {
        var playerStatistic = _playerStatistics.FirstOrDefault(x =>
            x.Name == playerName && x.CourseRevision == scorecard.CourseRevision);

        if (playerStatistic != null) 
            return playerStatistic;
        
        playerStatistic = PlayerStatistic.Create(scorecard.CourseId, scorecard.CourseRevision, playerName);
        _playerStatistics.Add(playerStatistic);

        return playerStatistic;
    }

    public IEnumerable<CourseSeason> GenerateCourseSeasons()
    {
        foreach (var courseSeason in _courseSeasons)
            courseSeason.Reset();
        
        foreach (var scorecard in _scorecards)
        {
            var courseSeason = FindOrCreateCourseSeason(scorecard);
            courseSeason.Add(scorecard);
        }

        return _courseSeasons;
    }
    
    private CourseSeason FindOrCreateCourseSeason(Scorecard scorecard)
    {
        var courseSeason = _courseSeasons.FirstOrDefault(x =>
            x.CourseId == scorecard.CourseId && x.Season == scorecard.GetSeason());

        if (courseSeason != null) 
            return courseSeason;
        
        courseSeason = CourseSeason.Create(scorecard.CourseId, scorecard.GetSeason());
        _courseSeasons.Add(courseSeason);

        return courseSeason;
    }
}