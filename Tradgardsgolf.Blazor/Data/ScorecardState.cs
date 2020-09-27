using Microsoft.AspNetCore.ProtectedBrowserStorage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tradgardsgolf.Blazor.Data
{
    public class HoleScore
    {
        public int Hole { get; set; }
        public int? Score { get; set; }

        public HoleScore()
        {

        }

        private HoleScore(int hole)
        {
            Hole = hole;
            Score = default;
        }

        public static HoleScore Create(int hole)
        {
            return new HoleScore(hole);
        }
    }

    public class PlayerScore
    {
        public string Name { get; set; }

        public IList<HoleScore> Scores { get; set; }


        public PlayerScore()
        {

        }

        private PlayerScore(string name, IList<HoleScore> scores)
        {
            Name = name;
            Scores = scores;
        }

        public static PlayerScore Create(string name, int holes)
        {
            var scores = new List<HoleScore>();

            for (int hole = 1; hole <= holes; hole++)
                scores.Add(HoleScore.Create(hole));

            return new PlayerScore(name, scores);
        }

        public int Total() => Scores.Select(x => x.Score.GetValueOrDefault(0)).Sum();
        public bool MissingScores() => Scores.Any(x => x.Score.HasValue == false);
    }

    public class ScorecardState
    {
        private readonly ProtectedSessionStorage _storage;            
        public async Task<Course> GetSelectedCourseAsync() => await _storage.GetAsync<Course>("SelectedCourse");
        public async Task SetSelectedCourseAsync(Course course) => await _storage.SetAsync("SelectedCourse", course);
        public async Task<IEnumerable<PlayerScore>> GetPlayersAsync() => await _storage.GetAsync<IEnumerable<PlayerScore>>("Players") ?? new List<PlayerScore>();
        public async Task SetPlayersAsync(IEnumerable<PlayerScore> players) => await _storage.SetAsync("Players", players);        

        public ScorecardState(ProtectedSessionStorage storage)
        {
            _storage = storage;
        }
    }
}
