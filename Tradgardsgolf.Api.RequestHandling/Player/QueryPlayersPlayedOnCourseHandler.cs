using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Contracts.Players;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;
using Tradgardsgolf.Core.Specifications.PlayerStatistic;

namespace Tradgardsgolf.Api.RequestHandling.Player;

public class QueryPlayersPlayedOnCourseHandler(IRepository repository)
    : IRequestHandler<QueryPlayersPlayedOnCourse, IEnumerable<PlayerResponse>>
{
    public async Task<IEnumerable<PlayerResponse>> Handle(QueryPlayersPlayedOnCourse request,
        CancellationToken cancellationToken)
    {
        return (await repository.ListAsync(Specs.PlayerStatistic.ByCourse(request.CourseId), cancellationToken))
            .GroupBy(x => x.Name)
            .Select(x => new
            {
                Name = x.Key, 
                TimesPlayed = x.Sum(statistic => statistic.TimesPlayed)
            })
            .OrderByDescending(x => x.TimesPlayed > 50)
            .ThenByDescending(x => x.TimesPlayed > 10)
            .ThenBy(x => x.Name)
            .Select(x => new PlayerResponse
            {
                Name = x.Name
            }).ToList();
    }
}