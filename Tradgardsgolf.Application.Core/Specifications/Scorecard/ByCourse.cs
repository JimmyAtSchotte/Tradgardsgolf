using System;
using Ardalis.Specification;

namespace Tradgardsgolf.Core.Specifications.Scorecard;

public class ByCourse : Specification<Entities.Scorecard>
{
    public ByCourse(Guid courseId)
    {
        Query.Where(x => x.CourseId == courseId);
    }
}