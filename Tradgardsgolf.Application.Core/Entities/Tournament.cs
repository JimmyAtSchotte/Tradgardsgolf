using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tradgardsgolf.Core.Entities
{
    [Table("tournament")]
    public class Tournament : BaseEntity<Tournament>
    {
        private ICollection<TournamentCourseDate> _tournamentCourseDates;
        private ICollection<TournamentRound> _tournamentRounds;

        [Key]
        public int Id { get; set; }
        
        [Column("strName")]
        public string Name { get; set; }
        
        public ICollection<TournamentCourseDate> TournamentCourseDates
        {
            get => _tournamentCourseDates ??= new List<TournamentCourseDate>();
            set => _tournamentCourseDates = value;
        }
        
        public ICollection<TournamentRound> TournamentRounds
        {
            get => _tournamentRounds ??= new List<TournamentRound>();
            set => _tournamentRounds = value;
        }
    }
}