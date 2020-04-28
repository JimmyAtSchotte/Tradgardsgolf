using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tradgardsgolf.Mobile.Core.Interfaces;

namespace Tradgardsgolf.Mobile.Core
{
    public sealed class SessionStorage : ISessionStorage
    {
        private static readonly Lazy<ISessionStorage>  lazy = new Lazy<ISessionStorage>(() => new SessionStorage(ApiRepository.Instance));
        public static ISessionStorage Instance { get { return lazy.Value; } }

        public IEnumerable<string> Courses => _apiRepository.GetCourses();

        private readonly IApiRepository _apiRepository;

        private SessionStorage(IApiRepository apiRepository)
        {
            _apiRepository = apiRepository;
        }
    }

    public sealed class ApiRepository : IApiRepository
    {
        private static readonly Lazy<IApiRepository> lazy = new Lazy<IApiRepository>(() => new ApiRepository());
        public static IApiRepository Instance { get { return lazy.Value; } }

        private ApiClient _apiClient;
        private string _token;

        private ApiRepository()
        {
            _apiClient = null;
            _token = null;
        }

        public Task<bool> Authenticate()
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsAuthenticated()
        {
            throw new NotImplementedException();
        }

        public void CreateApiClient(string baseUrl)
        {
            _apiClient = new ApiClient(baseUrl);
        }

        public Task<IEnumerable<string>> GetCourses()
        {
            throw new NotImplementedException();
        }
    }

    public class ApiClient
    {
        private readonly string _baseUrl;

        public ApiClient(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public TResult Get<TResult>()
        {
            throw new NotImplementedException();
        }

        public TResult Post<TModel, TResult>()
        {
            throw new NotImplementedException();
        }
    }
}
