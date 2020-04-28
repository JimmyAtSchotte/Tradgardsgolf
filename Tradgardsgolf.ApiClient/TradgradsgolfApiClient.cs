using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Tradgardsgolf.ApiClient
{
    public class TradgradsgolfApiClient
    {
        private static HttpClient _httpClient;

        internal Action OnUnauthorized { get; }

        public TradgradsgolfApiClient(Action<TradgradsgolfApiClientOptions> options)

        {
            var clientOptions = new TradgradsgolfApiClientOptions();
            options?.Invoke(clientOptions);

            if (clientOptions.Url == null)
                throw new ArgumentException($"{nameof(options)}{nameof(clientOptions.Url)} can't be null");

            OnUnauthorized = clientOptions.OnUnathorized;

            var handler = new HttpClientHandler();
            clientOptions.ClientHandlerConfiguration?.Invoke(handler);

            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(clientOptions.Url + (clientOptions.Url.EndsWith("/")? "" : "/")),
                Timeout = clientOptions?.Timeout ?? TimeSpan.FromSeconds(30)
            };
        }

        internal void SetAuthenticationHeaderValue(AuthenticationHeaderValue authentication)
        {
            _httpClient.DefaultRequestHeaders.Authorization = authentication;
        }

        internal async Task<HttpResponseMessage> PostAsync<T>(string endpoint, T model)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, endpoint);

            var json = JsonConvert.SerializeObject(model);
            request.Content = new StringContent(json, Encoding.UTF8);
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json; charset=utf-8");
            return await _httpClient.SendAsync(request);
        }

        internal async Task<HttpResponseMessage> GetAsync(string endpoint)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
            return await _httpClient.SendAsync(request);
        }

        internal async Task<IResponse> Response(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                InvokeUnSuccessStatusCodeEvents(response.StatusCode);

                return new Response(response.StatusCode, content);
            }

            return new Response(response.StatusCode, string.Empty);
        }

        internal async Task<IResponse<T>> Response<T>(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                InvokeUnSuccessStatusCodeEvents(response.StatusCode);

                return new Response<T>(default, response.StatusCode, content);
            }

            var result = JsonConvert.DeserializeObject<T>(content);

            return new Response<T>(result, response.StatusCode, string.Empty);
        }

        internal IResponse Response(Exception exception)
        {
            InvokeUnSuccessStatusCodeEvents(HttpStatusCode.InternalServerError);

            return new Response(HttpStatusCode.InternalServerError, exception.Message);
        }

        internal IResponse<T> Response<T>(Exception exception)
        {
            InvokeUnSuccessStatusCodeEvents(HttpStatusCode.InternalServerError);

            return new Response<T>(default, HttpStatusCode.InternalServerError, exception.Message);
        }

        private void InvokeUnSuccessStatusCodeEvents(HttpStatusCode statusCode)
        {
            switch (statusCode)
            {
                case HttpStatusCode.Unauthorized:
                    OnUnauthorized?.Invoke();
                    break;
                default:
                    break;
            }
        }

    }
}
