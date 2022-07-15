using System;
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
}