using System;
using Ardalis.Specification;

namespace Tradgardsgolf.Core.Specifications.PlayerStatistic;


public static partial class PlayerStatisticsSpecificationExtensions
{
    public static ISpecification<Entities.PlayerStatistic> ByCourse(this SpecificationSet<Entities.PlayerStatistic> set,
        Guid courseId)
        => new ByCourse(courseId);
}

internal sealed class ByCourse : SpecificationEquatable<Entities.PlayerStatistic, ByCourse>
{
    public ByCourse(Guid courseId) : base(courseId)
    {
        Query.Where(x => x.CourseId == courseId);
    }
}