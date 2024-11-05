using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Contracts.Statistics;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;
using Tradgardsgolf.Core.Specifications.Scorecard;

namespace Tradgardsgolf.Api.RequestHandling.Course;

public class QueryCourseStatisticsHandler(IRepository repository)
    : IRequestHandler<QueryCourseStatistics, CourseStatisticResponse>
{
    public async Task<CourseStatisticResponse> Handle(QueryCourseStatistics request,
        CancellationToken cancellationToken)
    {
        var scorecards = await repository.ListAsync(Specs.Scorecard.ByCourse(request.CourseId), cancellationToken);

        return new CourseStatisticResponse
        {
            Scorecards = scorecards.Select(scorecard => new ScorecardResponse
            {
                Date = scorecard.Date,
                Scores = scorecard.Scores.SelectMany(keyValuePair => keyValuePair.Value.Select((score, hole) =>
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