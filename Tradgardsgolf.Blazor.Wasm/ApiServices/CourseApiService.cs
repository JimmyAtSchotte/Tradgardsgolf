using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Tradgardsgolf.Api.Shared;
using Tradgardsgolf.Contracts.Course;

namespace Tradgardsgolf.Blazor.Wasm.ApiServices
{
    public interface ICourseApiService
    {
        Task SaveScorecard(Course courseModel, IEnumerable<PlayerScores> playerScores);
    }

    public class CourseApiService : ICourseApiService
    {
        private readonly HttpClient _httpClient;

        public CourseApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

      
 

        public async Task SaveScorecard(Course courseModel, IEnumerable<PlayerScores> playerScores)
        {
            await _httpClient.PostAsJsonAsync($"Courses/{courseModel.Id}/Scorecards", playerScores);
        }
    }


}
