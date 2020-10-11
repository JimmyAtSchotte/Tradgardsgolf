using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tradgardsgolf.Infrastructure.Context
{
    public class Round : BaseEntity<Round>
    {
        [Key]
        public int Id { get; set; }

        [Column("intCourseId")]
        public int CourseId { get; private set; }
        public Course Course { get; private set; }

        [Column("dtmDate")]
        public DateTime Date { get; private set; }
        
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
            return new RoundScore(this, player, hole, score);
        }
    }
}
