using System;
using System.Collections.Generic;

namespace Tradgardsgolf.Core.Entities
{
    public class Scorecard : BaseEntity<Scorecard>
    {
        private IDictionary<string, int[]> _scores;
        public Guid Id { get; set; }
        public Guid CourseId { get; private set; }
        public DateTime Date { get; private set; }
        public Course Course { get; private set; }

        public IDictionary<string, int[]> Scores
        {
            get => _scores ??= new Dictionary<string, int[]>();
            set => _scores = value;
        }

        public Guid TournamentId { get; set; }

        private Scorecard()
        {

        }

        private Scorecard(Course course)
        {
            CourseId = course.Id;
            Course = course;
            Date = DateTime.Now;
        }

        public void AddPlayerScores(string player, params int[] scores)
        {
            Scores.Add(player, scores);
        }

        public static Scorecard Create(Course course)
        {
            return new Scorecard(course);
        }
    }
}
