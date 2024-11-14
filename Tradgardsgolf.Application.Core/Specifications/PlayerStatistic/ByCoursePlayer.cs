using System;
using System.Diagnostics.CodeAnalysis;
using Ardalis.Specification;

namespace Tradgardsgolf.Core.Specifications.PlayerStatistic;


[SuppressMessage("ReSharper", "UnusedParameter.Global")]
public static partial class PlayerStatisticsSpecificationExtensions
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