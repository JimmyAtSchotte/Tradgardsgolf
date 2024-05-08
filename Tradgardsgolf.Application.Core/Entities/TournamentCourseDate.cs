using System;

namespace Tradgardsgolf.Core.Entities;

public class TournamentCourseDate : BaseEntity<TournamentCourseDate>
{
    private TournamentCourseDate() { }

    private TournamentCourseDate(Course course, DateTime date)
    {
        CourseId = course.Id;
        Course = course;
        Date = date;
    }

    public Guid CourseId { get; init; }
    public DateTime Date { get; init; }
    public Course Course { get; init; }

    public static TournamentCourseDate Create(Course course, DateTime date)
    {
        return new TournamentCourseDate(course, date);
    }
}