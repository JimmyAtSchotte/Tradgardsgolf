using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Ardalis.Specification;

namespace Tradgardsgolf.Core.Specifications.Tournament;

[SuppressMessage("ReSharper", "UnusedParameter.Global")]
[SuppressMessage("ReSharper", "PartialTypeWithSinglePart")]
public static partial class TournamentSpecificationExtensions
{
    public static ISpecification<Entities.Tournament> ByCourseAndDate(this SpecificationSet<Entities.Tournament> set,
        Guid courseId, DateTime date)
        => new ByCourseAndDate(courseId, date);
}

internal sealed class ByCourseAndDate : SpecificationEquatable<Entities.Tournament, ByCourseAndDate>
{
    public ByCourseAndDate(Guid courseId, DateTime date) : base(courseId, date)
    {
        Query.PostProcessingAction(tournaments => tournaments.Where(x => x.TournamentCourseDates.Any(courseDate =>
                     courseDate.CourseId == courseId && courseDate.Date == date)));
    }
}