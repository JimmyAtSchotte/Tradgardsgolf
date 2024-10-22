using System;
using Ardalis.Specification;
using Tradgardsgolf.Core.Specifications.Course;

namespace Tradgardsgolf.Core.Specifications.Scorecard;

public class ById : SpecificationEquatable<Entities.Scorecard, ById>
{
    public ById(Guid scorecardId) : base(scorecardId)
    {
        Query.Where(x => x.Id == scorecardId);
    }
}