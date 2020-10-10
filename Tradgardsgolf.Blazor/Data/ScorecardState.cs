using Microsoft.AspNetCore.ProtectedBrowserStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tradgardsgolf.Blazor.Data
{

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
