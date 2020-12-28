using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Tradgardsgolf.Api.Shared;
using Tradgardsgolf.Blazor.Wasm.Data;

namespace Tradgardsgolf.Blazor.Wasm.ApiServices
{
    public interface ICourseApiService
    {
        Task<IEnumerable<Course>> ListAll();
        Task<IEnumerable<Player>> Players(Course course);
        Task SaveScorecard(Course course, IEnumerable<PlayerScore> playerScores);
        Task<CourseStatistic> Statistics(Course course);
    }

    public class CourseApiService : ICourseApiService
    {
        private readonly HttpClient _httpClient;

        public CourseApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Course>> ListAll()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Course>>("Courses");
        }

        public async Task<IEnumerable<Player>> Players(Course course)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Player>>($"Courses/{course.Id}/Players");
        }
        
        public async Task<CourseStatistic> Statistics(Course course)
        {
            return await _httpClient.GetFromJsonAsync<CourseStatistic>($"Courses/{course.Id}/Statistics");
        }

        public async Task SaveScorecard(Course course, IEnumerable<PlayerScore> playerScores)
        {
            await _httpClient.PostAsJsonAsync($"Courses/{course.Id}/Scorecards", playerScores);
        }
    }


}
