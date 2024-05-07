using System;
using Ardalis.Specification;

namespace Tradgardsgolf.Core.Specifications.Scorecard;

public class ById : Specification<Entities.Scorecard>
{
    public ById(Guid scorecardId)
    {
        Query.Where(x => x.Id == scorecardId);
    }
}

