using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Contracts;

namespace Tradgardsgolf.Blazor.Wasm.ApiServices
{
    public interface IApiDispatcher
    {
        Task<TResponse?> Dispatch<TResponse>(IRequest<TResponse> request);
    }

    public class ApiDispatcher : IApiDispatcher
    {
        private readonly HttpClient _httpClient;

        public ApiDispatcher(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
      
        public async Task<TResponse?> Dispatch<TResponse>(IRequest<TResponse> request)
        {
            var dispatchUrl = DispatchUrlBuilder.Build(request);
            var requestBody = JsonSerializer.Serialize(request, request.GetType());

            try
            {
                var response = await _httpClient.PostAsync(dispatchUrl, new StringContent(requestBody, Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<TResponse>()
                       ?? throw new InvalidOperationException("Could not read response message");
            }
            catch (Exception e)
            {
                
            }

            return default;
        }
    }

    public class DispatchUrlBuilder
    {
        private static readonly string ContractNamespacePrefix = typeof(IContractsNamespaceMarker).Namespace;
        
        public static string Build<TResponse>(IRequest<TResponse> request)
        {
            var requestType = request.GetType();
            var domain = requestType.Namespace.Substring(ContractNamespacePrefix.Length + 1);
            
            return $"/api/dispatch/{domain}/{requestType.Name}";
        }
    }
}