using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tradgardsgolf.Core.Entities;

[Table("tournamentround")]
public class TournamentRound : BaseEntity<TournamentRound>
{
    [Key]
    public int Id { get; set; }
        
    [Column("intTournamentId")]
    public int TournamentId { get; set; }
        
    [Column("intCourseId")]
    public int RoundId { get; set; }

    public Tournament Tournament { get; set; }
    public Round Round { get; set; }
}