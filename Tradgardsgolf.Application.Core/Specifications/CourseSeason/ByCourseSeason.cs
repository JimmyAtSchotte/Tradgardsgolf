using System;
using System.Diagnostics.CodeAnalysis;
using Ardalis.Specification;

namespace Tradgardsgolf.Core.Specifications.CourseSeason;


[SuppressMessage("ReSharper", "UnusedParameter.Global")]
public static partial class CourseSeasonSpecificationExtensions
{
    public static ISpecification<Entities.CourseSeason> ByCourseSeason(this SpecificationSet<Entities.CourseSeason> set,
        Guid courseId, string season)
        => new ByCourseSeason(courseId, season);
}

internal sealed class ByCourseSeason : SpecificationEquatable<Entities.CourseSeason, ByCourse>
{
    public ByCourseSeason(Guid courseId, string season) : base(courseId, season)
    {
        Query.Where(x => x.CourseId == courseId && x.Season == season);
    }
}