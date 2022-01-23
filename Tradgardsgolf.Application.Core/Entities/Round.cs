using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tradgardsgolf.Core.Entities
{
    [Table("round")]

    public class Round : BaseEntity<Round>
    {
        private ICollection<RoundScore> _roundScores;

        [Key]
        public int Id { get; set; }

        [Column("intCourseId")]
        public int CourseId { get; private set; }
        public Course Course { get; private set; }

        [Column("dtmDate")]
        public DateTime Date { get; private set; }


        public virtual ICollection<RoundScore> RoundScores
        {
            get => _roundScores ??= new List<RoundScore>();
            set => _roundScores = value;
        }

        private Round()
        {

        }

        internal Round(Course course)
        {
            CourseId = course.Id;
            Course = course;
            Date = DateTime.Now;
        }

        public RoundScore CreateRoundScore(Player player, int hole, int score)
        {
            var roundScore = new RoundScore(this, player, hole, score);
            
            RoundScores.Add(roundScore);
            
            return roundScore;
        }
    }
}
