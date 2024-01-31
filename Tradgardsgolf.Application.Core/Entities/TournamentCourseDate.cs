using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tradgardsgolf.Core.Entities;

[Table("tournamentcoursedate")]
public class TournamentCourseDate : BaseEntity<TournamentCourseDate>
{
    [Key]
    public int Id { get; set; }
        
    [Column("intTournamentId")]
    public int TournamentId { get; set; }
        
    [Column("intCourseId")]
    public int CourseId { get; set; }
        
    [Column("dtmDate")]
    public DateTime Date { get; set; }

    public Tournament Tournament { get; set; }
    public Course Course { get; set; }
}