using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Tradgardsgolf.Blazor.Wasm.Data;

namespace Tradgardsgolf.Blazor.Wasm.ServiceAdapters
{
    public interface IScorecardServiceAdapter
    {
        Task Add(Course course, IEnumerable<PlayerScore> playerScores);
    }

    public class ScorecardServiceAdapter : IScorecardServiceAdapter
    {
        private readonly HttpClient _httpClient;

        public ScorecardServiceAdapter(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task Add(Course course, IEnumerable<PlayerScore> playerScores)
        {
            await _httpClient.PostAsJsonAsync($"Courses/{course.Id}", playerScores);
        }
    }
}
