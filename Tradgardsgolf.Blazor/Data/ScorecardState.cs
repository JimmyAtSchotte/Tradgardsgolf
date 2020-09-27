using Microsoft.AspNetCore.ProtectedBrowserStorage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tradgardsgolf.Blazor.Data
{
    public class HoleScoreCollection : IList<HoleScore>
    {
        private readonly IList<HoleScore> _holeScores;

        public HoleScoreCollection()
        {
            _holeScores = new List<HoleScore>();
        }

        public HoleScore this[int index] { 
            get =>  _holeScores.FirstOrDefault(x => x.Hole == index);            
            set =>  this[index] = value;            
        }

        public int Count => _holeScores.Count;
        public bool IsReadOnly => _holeScores.IsReadOnly;
        public void Add(HoleScore item) => _holeScores.Add(item);
        public void Clear() => _holeScores.Clear();
        public bool Contains(HoleScore item) => _holeScores.Contains(item);
        public void CopyTo(HoleScore[] array, int arrayIndex) => _holeScores.CopyTo(array, arrayIndex);
        public IEnumerator<HoleScore> GetEnumerator() => _holeScores.GetEnumerator();

        public int IndexOf(HoleScore item) => _holeScores.IndexOf(item);
        public void Insert(int index, HoleScore item) => _holeScores.Insert(index, item);

        public bool Remove(HoleScore item) => _holeScores.Remove(item);
        public void RemoveAt(int index) => _holeScores.RemoveAt(index);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }


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

        public HoleScoreCollection Scores { get; set; }


        public PlayerScore()
        {

        }

        private PlayerScore(string name, HoleScoreCollection scores)
        {
            Name = name;
            Scores = scores;
        }

        public static PlayerScore Create(string name, int holes)
        {
            var scores = new HoleScoreCollection();

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
