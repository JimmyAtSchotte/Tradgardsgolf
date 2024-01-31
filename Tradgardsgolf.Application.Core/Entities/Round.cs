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
        public int CourseId { get; private set; }
        public DateTime Date { get; private set; }

        public Course Course { get; private set; }

        public ICollection<RoundScore> RoundScores
        {
            get => _roundScores ??= new List<RoundScore>();
            set => _roundScores = value;
        }

        private Round()
        {

        }

        private Round(Course course)
        {
            CourseId = course.Id;
            Course = course;
            Date = DateTime.Now;
        }

        public RoundScore CreateRoundScore(Player player, int hole, int score)
        {
            var roundScore = RoundScore.Create(this, player, hole, score);
            RoundScores.Add(roundScore);
            return roundScore;
        }

        public static Round Create(Course course)
        {
            return new Round(course);
        }
    }
}
