using System.Collections.Generic;
using System.Threading.Tasks;
using Tradgardsgolf.Blazor.Wasm.Data;

namespace Tradgardsgolf.Blazor.Wasm.State
{
    public class ScorecardState
    {
        private readonly IStorage _storage;
        public async Task<Course> GetSelectedCourseAsync() => await _storage.GetAsync<Course>("SelectedCourse");
        public async Task SetSelectedCourseAsync(Course course) => await _storage.SetAsync("SelectedCourse", course);
        public async Task<IEnumerable<PlayerScore>> GetPlayersAsync() => await _storage.GetAsync<IEnumerable<PlayerScore>>("Players") ?? new List<PlayerScore>();
        public async Task SetPlayersAsync(IEnumerable<PlayerScore> players) => await _storage.SetAsync("Players", players);

        public async Task ResetScores()
        {
            var players = await GetPlayersAsync();

            foreach (var player in players)
                foreach (var score in player.Scores)
                    score.Score = null;

            await SetPlayersAsync(players);
        }

        public ScorecardState(IStorage storage)
        {
            _storage = storage;
        }
    }
}
