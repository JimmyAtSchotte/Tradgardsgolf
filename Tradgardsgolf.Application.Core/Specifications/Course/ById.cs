using System;
using Ardalis.Specification;

namespace Tradgardsgolf.Core.Specifications.Course;

public class ById : SpecificationEquatable<Entities.Course, ById>
{
    public ById(Guid courseId) : base(courseId)
    {
        Query.Where(x => x.Id == courseId);
    }
}