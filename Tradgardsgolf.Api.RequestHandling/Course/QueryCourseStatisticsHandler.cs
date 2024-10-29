using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Contracts.Statistics;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;
using Tradgardsgolf.Core.Specifications.Scorecard;

namespace Tradgardsgolf.Api.RequestHandling.Course;

public class QueryCourseStatisticsHandler(IRepository<Core.Entities.Scorecard> scorecards)
    : IRequestHandler<QueryCourseStatistics, CourseStatisticResponse>
{
    public async Task<CourseStatisticResponse> Handle(QueryCourseStatistics request,
        CancellationToken cancellationToken)
    {
        var rounds = await scorecards.ListAsync(Specs.Scorecard.ByCourse(request.CourseId), cancellationToken);

        return new CourseStatisticResponse
        {
            Scorecards = rounds.Select(round => new ScorecardResponse
            {
                Date = round.Date,
                Scores = round.Scores.SelectMany(keyValuePair => keyValuePair.Value.Select((score, hole) =>
                    new HoleScoreResponse
                    {
                        Player = keyValuePair.Key,
                        Hole = hole + 1,
                        Score = score
                    }))
            })
        };
    }
}