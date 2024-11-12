using System;
using System.Collections.Generic;

namespace Tradgardsgolf.Core.Entities;

public class Tournament : BaseEntity
{
    private List<TournamentCourseDate> _tournamentCourseDates;
    public string Name { get; set; }
    public List<TournamentCourseDate> TournamentCourseDates
    {
        get => _tournamentCourseDates ??= new List<TournamentCourseDate>();
        set => _tournamentCourseDates = value;
    }
    
    private Tournament() { }

    private Tournament(string name)
    {
        Name = name;
    }



    public void AddCourseDate(Guid courseId, DateTime date)
    {
        TournamentCourseDates.Add(TournamentCourseDate.Create(courseId, date));
    }

    public static Tournament Create(string name)
    {
        return new Tournament(name);
    }
}