using System;
using Ardalis.Specification;

namespace Tradgardsgolf.Core.Specifications.PlayerStatistic;


public static partial class SpecificationSetExtensions
{
    public static ISpecification<Entities.PlayerStatistic> ByCourse(this SpecificationSet<Entities.PlayerStatistic> set,
        Guid courseId, int revision)
        => new ByCourse(courseId, revision);
}

internal sealed class ByCourse : SpecificationEquatable<Entities.PlayerStatistic, ByCourse>
{
    public ByCourse(Guid courseId, int revision) : base(courseId, revision)
    {
        Query.Where(x => x.CourseId == courseId && x.CourseRevision == revision);
    }
}