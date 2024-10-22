using System;
using Ardalis.Specification;
using Tradgardsgolf.Core.Specifications.Course;

namespace Tradgardsgolf.Core.Specifications.Scorecard;

public class ByCourse : SpecificationEquatable<Entities.Scorecard, ByCourse>
{
    public ByCourse(Guid courseId) : base(courseId)
    {
        Query.Where(x => x.CourseId == courseId);
    }
}