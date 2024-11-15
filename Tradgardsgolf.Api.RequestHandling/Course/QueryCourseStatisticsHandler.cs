using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Contracts.Statistics;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;
using Tradgardsgolf.Core.Specifications.CourseSeason;
using Tradgardsgolf.Core.Specifications.PlayerStatistic;

namespace Tradgardsgolf.Api.RequestHandling.Course;

[SuppressMessage("ReSharper", "UnusedType.Global")]
public class QueryCourseStatisticsHandler(IRepository repository)
    : IRequestHandler<QueryCourseStatistics, CourseStatisticResponse>
{
    public async Task<CourseStatisticResponse> Handle(QueryCourseStatistics request,
        CancellationToken cancellationToken)
    {
        var courseSeasons = await repository.ListAsync(Specs.CourseSeason.ByCourse(request.CourseId), cancellationToken);
        var playerStatistics = await repository.ListAsync(Specs.PlayerStatistic.ByCourseRevision(request.CourseId, request.Revision), cancellationToken);

        return new CourseStatisticResponse
        {
            Seasons = courseSeasons.Select(x => new CourseSeasonResposne
            {
                Season = x.Season,
                Players = x.Players
            }),
            PlayerStatistics = playerStatistics.Select(x => new PlayerStatisticResponse
            {
                AverageScore = x.AverageScore,
                Name = x.Name,
                TimesPlayed = x.TimesPlayed,
                BestScore = new BestScoreResponse
                {
                    Date = x.BestScore.Date,
                    Score = x.BestScore.Score
                },
                HoleStatistics = x.HoleStatistics.Select(h => new HoleStatisticResponse
                {
                    AverageScore = h.AverageScore,
                    HoleInOnes = h.HoleInOnes
                })
            })
        };
    }
}