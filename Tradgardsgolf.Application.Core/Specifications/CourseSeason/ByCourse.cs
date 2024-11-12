using System;
using Ardalis.Specification;

namespace Tradgardsgolf.Core.Specifications.CourseSeason;


public static partial class CourseSeasonSpecificationExtensions
{
    public static ISpecification<Entities.CourseSeason> ByCourse(this SpecificationSet<Entities.CourseSeason> set,
        Guid courseId)
        => new ByCourse(courseId);
}

internal sealed class ByCourse : SpecificationEquatable<Entities.CourseSeason, ByCourse>
{
    public ByCourse(Guid courseId) : base(courseId)
    {
        Query.Where(x => x.CourseId == courseId);
    }
}