using System;
using Ardalis.Specification;

namespace Tradgardsgolf.Core.Specifications.CourseSeason;


public static partial class CourseSeasonSpecificationExtensions
{
    public static ISpecification<Entities.CourseSeason> ByCourseSeason(this SpecificationSet<Entities.CourseSeason> set,
        Guid courseId, int season)
        => new ByCourseSeason(courseId, season);
}

internal sealed class ByCourseSeason : SpecificationEquatable<Entities.CourseSeason, ByCourse>
{
    public ByCourseSeason(Guid courseId, int season) : base(courseId, season)
    {
        Query.Where(x => x.CourseId == courseId && x.Season == season);
    }
}