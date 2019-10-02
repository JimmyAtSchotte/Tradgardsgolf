using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tradgardsgolf.Infrastructure.EntityBuilder;
using Tradgardsgolf.Infrastructure.SharedKernel;

namespace Tradgardsgolf.Infrastructure.Entities
{
    public class Round : BaseEntity<Round>
    {
        [Key]
        public int Id { get; internal set; }

        [Column("intCourseId")]
        public int CourseId { get; internal set; }
        public Course Course { get; internal set; }

        [Column("dtmDate")]
        public DateTime Date { get; internal set; }

        public ICollection<RoundScore> RoundScores { get; set; }

        private Round()
        {
            Date = DateTime.Now;
            RoundScores = new HashSet<RoundScore>();
        }

        public static Round Create(Action<RoundBuilder> options)
        {
            var round = new Round();
            round.SetOptions(options);

            return round;
        }


    }
}
