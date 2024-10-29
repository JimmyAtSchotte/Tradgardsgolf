using System;
using Ardalis.Specification;

namespace Tradgardsgolf.Core.Specifications.Course;


public static partial class SpecificationSetExtensions
{
    public static ISpecification<Entities.Course> ById(this SpecificationSet<Entities.Course> set,
        Guid courseId)
        => new ById(courseId);
}

internal sealed class ById : SpecificationEquatable<Entities.Course, ById>
{
    public ById(Guid courseId) : base(courseId)
    {
        Query.Where(x => x.Id == courseId);
    }
}