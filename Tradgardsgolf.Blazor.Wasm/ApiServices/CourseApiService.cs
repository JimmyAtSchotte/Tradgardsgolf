using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Tradgardsgolf.Api.Shared;

namespace Tradgardsgolf.Blazor.Wasm.ApiServices
{
    public interface ICourseApiService
    {
        Task<IEnumerable<CourseModel>> ListAll();
        Task SaveScorecard(CourseModel courseModel, IEnumerable<PlayerScores> playerScores);
    }

    public class CourseApiService : ICourseApiService
    {
        private readonly HttpClient _httpClient;

        public CourseApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CourseModel>> ListAll()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<CourseModel>>("Courses");
        }
 

        public async Task SaveScorecard(CourseModel courseModel, IEnumerable<PlayerScores> playerScores)
        {
            await _httpClient.PostAsJsonAsync($"Courses/{courseModel.Id}/Scorecards", playerScores);
        }
    }


}
