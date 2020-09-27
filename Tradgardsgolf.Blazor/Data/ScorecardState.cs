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

        public async Task<IEnumerable<string>> GetPlayersAsync() => await _storage.GetAsync<IEnumerable<string>>("Players") ?? new List<string>();
        public async Task SetPlayersAsync(IEnumerable<string> players) => await _storage.SetAsync("Players", players);

        

        public ScorecardState(ProtectedSessionStorage storage)
        {
            _storage = storage;
        }
    }
}
