using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Tradgardsgolf.Core.Entities;

[SuppressMessage("ReSharper", "UnusedMember.Local")]
public class Tournament : BaseEntity
{
    private List<TournamentCourseDate> _tournamentCourseDates;
    public string Name { get; init; }
    public List<TournamentCourseDate> TournamentCourseDates
    {
        get => _tournamentCourseDates ??= [];
        init => _tournamentCourseDates = value;
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