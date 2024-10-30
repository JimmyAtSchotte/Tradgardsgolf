﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Contracts.Players;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;
using Tradgardsgolf.Core.Specifications.Scorecard;

namespace Tradgardsgolf.Api.RequestHandling.Player;

public class QueryPlayersPlayedOnCourseHandler(IRepository<Core.Entities.Scorecard> scorecards)
    : IRequestHandler<QueryPlayersPlayedOnCourse, IEnumerable<PlayerResponse>>
{
    public async Task<IEnumerable<PlayerResponse>> Handle(QueryPlayersPlayedOnCourse request,
        CancellationToken cancellationToken)
    {
        return (await scorecards.ListAsync(Specs.Scorecard.ByCourse(request.CourseId), cancellationToken))
            .SelectMany(x => x.Scores.Keys)
            .GroupBy(x => x)
            .OrderByDescending(x => x.Count() > 50)
            .ThenByDescending(x => x.Count() > 10)
            .ThenBy(x => x.Key)
            .Select(x => new PlayerResponse
            {
                Name = x.Key
            }).ToList();
    }
}