using System;
using Ardalis.Specification;

namespace Tradgardsgolf.Core.Specifications.PlayerStatistic;


public static partial class SpecificationSetExtensions
{
    public static ISpecification<Entities.PlayerStatistic> ByCoursePlayer(this SpecificationSet<Entities.PlayerStatistic> set,
        Guid courseId, int revision, string playername)
        => new ByCoursePlayer(courseId, revision, playername);
}

internal sealed class ByCoursePlayer : SpecificationEquatable<Entities.PlayerStatistic, ByCoursePlayer>
{
    public ByCoursePlayer(Guid courseId, int revision, string playername) : base(courseId, revision, playername)
    {
        Query.Where(x => x.CourseId == courseId && x.CourseRevision == revision && x.Name == playername);
    }
}