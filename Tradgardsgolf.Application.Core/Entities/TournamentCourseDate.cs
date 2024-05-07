using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tradgardsgolf.Core.Entities;

public class TournamentCourseDate : BaseEntity<TournamentCourseDate>
{
    public Guid CourseId { get; set; }
    public DateTime Date { get; set; }
    
    public Course Course { get; set; }
}