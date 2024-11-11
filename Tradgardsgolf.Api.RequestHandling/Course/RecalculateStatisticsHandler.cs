using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Contracts.Course;
using Tradgardsgolf.Core.Entities;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;
using Tradgardsgolf.Core.Specifications.Scorecard;

namespace Tradgardsgolf.Api.RequestHandling.Course;

public class RecalculateStatisticsHandler : IRequestHandler<RecalculateStatisticsCommand, Unit>
{
    private readonly IRepository repository;
    
    public RecalculateStatisticsHandler(IRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Unit> Handle(RecalculateStatisticsCommand request, CancellationToken cancellationToken)
    {
        var course = await repository.FirstOrDefaultAsync(Specs.ById<Core.Entities.Course>(request.CourseId), cancellationToken);
        var scorecards = (await repository.ListAsync(Specs.Scorecard.ByCourse(course.Id), cancellationToken)).ToList();

        await AddPlayerStatistics(cancellationToken, scorecards, course);
        await AddCourseSeasons(cancellationToken, scorecards, course);
        
        return Unit.Value;
    }

    private async Task AddCourseSeasons(CancellationToken cancellationToken, List<Core.Entities.Scorecard> scorecards, Core.Entities.Course course)
    {
        var seasons = scorecards.Select(x => x.Date.Year).Distinct().Select(season => CourseSeason.Create(course.Id, season)).ToList();
        
        foreach (var scorecard in scorecards)
        {
            var season = scorecard.Date.Year;
            var courseSeason = seasons.FirstOrDefault(s => s.CourseId == scorecard.CourseId && s.Season == season)
                    ?? throw new Exception($"Season {season} on course {scorecard.CourseId} does not exist");
           
            courseSeason.Add(scorecard);
        }

        foreach (var season in seasons)
        {
            await repository.AddAsync(season, cancellationToken);
        }
    }

    private async Task AddPlayerStatistics(CancellationToken cancellationToken, List<Core.Entities.Scorecard> scorecards, Core.Entities.Course course)
    {
        var playerStatistics = new List<PlayerStatistic>();

        if (course.Revision == 0 && course.ScoreReset.HasValue)
        {
            course.ResetScore(course.ScoreReset.Value);

            foreach (var scorecard in scorecards.Where(x => x.Date > course.ScoreReset))
                scorecard.CourseRevision = course.Revision;
            
            await repository.UpdateRangeAsync(scorecards.Where(x => x.CourseRevision == course.Revision).ToArray(), cancellationToken);
        }

        foreach (var revision in scorecards.Select(x => x.CourseRevision).Distinct())
        {
            playerStatistics.AddRange(scorecards.Where(x => x.CourseRevision == revision)
                .SelectMany(x => x.Scores.Keys)
                .Distinct()
                .Select(player => PlayerStatistic.Create(course.Id, revision, player)));
        }
        
        foreach (var scorecard in scorecards)
        {
            foreach (var score in scorecard.Scores)
            {
                var playerName = score.Key;
                var playerStats = playerStatistics.FirstOrDefault(x => x.Name == playerName && x.CourseRevision == scorecard.CourseRevision)
                                        ?? throw new Exception($"Player {playerName} does not exist");
              
                playerStats.Add(scorecard);
            }
        }

        foreach (var playerStatistic in playerStatistics)
        {
            await repository.AddAsync(playerStatistic, cancellationToken);
        }
    }
}