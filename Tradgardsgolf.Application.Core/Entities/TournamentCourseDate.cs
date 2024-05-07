using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tradgardsgolf.Core.Entities;

public class TournamentCourseDate : BaseEntity<TournamentCourseDate>
{
    public Guid CourseId { get; init; }
    public DateTime Date { get; init; }
    public Course Course { get; init; }

    private TournamentCourseDate()
    {
        
    }

    private TournamentCourseDate(Course course, DateTime date)
    {
        CourseId = course.Id;
        Course = course;
        Date = date;
    }

    public static TournamentCourseDate Create(Course course, DateTime date)
    {
        return new TournamentCourseDate(course, date);
    }
}