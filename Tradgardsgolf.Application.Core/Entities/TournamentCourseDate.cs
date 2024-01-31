using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tradgardsgolf.Core.Entities;

[Table("tournamentcoursedate")]
public class TournamentCourseDate : BaseEntity<TournamentCourseDate>
{
    [Key]
    public int Id { get; set; }
        
    public int TournamentId { get; set; }
    public int CourseId { get; set; }
    public DateTime Date { get; set; }
    public Tournament Tournament { get; set; }
    public Course Course { get; set; }
}