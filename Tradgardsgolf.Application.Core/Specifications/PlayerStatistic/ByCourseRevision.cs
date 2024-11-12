using System;
using Ardalis.Specification;

namespace Tradgardsgolf.Core.Specifications.PlayerStatistic;

public static partial class PlayerStatisticsSpecificationExtensions
{
    public static ISpecification<Entities.PlayerStatistic> ByCourseRevision(this SpecificationSet<Entities.PlayerStatistic> set,
        Guid courseId, int revision)
        => new ByCourseRevision(courseId, revision);
}

internal sealed class ByCourseRevision : SpecificationEquatable<Entities.PlayerStatistic, ByCourseRevision>
{
    public ByCourseRevision(Guid courseId, int revision) : base(courseId, revision)
    {
        Query.Where(x => x.CourseId == courseId && x.CourseRevision == revision);
    }
}