using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Tradgardsgolf.Blazor.Wasm.Data;

namespace Tradgardsgolf.Blazor.Wasm.ServiceAdapters
{
    public interface ICourseServiceAdapter
    {
        Task<IEnumerable<Course>> ListAll();
        Task<IEnumerable<Player>> Players(Course course);
    }

    public class CourseServiceAdapter : ICourseServiceAdapter
    {
        private readonly HttpClient _httpClient;

        public CourseServiceAdapter(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Course>> ListAll()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Course>>("Courses");
        }

        public async Task<IEnumerable<Player>> Players(Course course)
        {
            return Enumerable.Empty<Player>();
        }
    }
}
