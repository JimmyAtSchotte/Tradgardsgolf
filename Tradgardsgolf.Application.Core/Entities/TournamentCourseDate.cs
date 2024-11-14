using System;
using System.Diagnostics.CodeAnalysis;

namespace Tradgardsgolf.Core.Entities;

[SuppressMessage("ReSharper", "UnusedMember.Local")]
[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
public class TournamentCourseDate
{
    private TournamentCourseDate() { }
    
    public Guid CourseId { get; init; }
    public DateTime Date { get; init; }

    private TournamentCourseDate(Guid courseId, DateTime date)
    {
        CourseId = courseId;
        Date = date;
    }

    public static TournamentCourseDate Create(Guid courseId, DateTime date)
    {
        return new TournamentCourseDate(courseId, date);
    }
}