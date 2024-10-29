using System;
using System.Collections.Generic;

namespace Tradgardsgolf.Core.Entities;

public class Tournament : BaseEntity<Tournament>
{
    private Tournament() { }

    private Tournament(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
    public List<TournamentCourseDate> TournamentCourseDates { get; set; }

    public void AddCourseDate(Course course, DateTime date)
    {
        if (TournamentCourseDates is null)
            TournamentCourseDates = new List<TournamentCourseDate>();

        TournamentCourseDates.Add(TournamentCourseDate.Create(course, date));
    }

    public static Tournament Create(string name)
    {
        return new Tournament(name);
    }
}