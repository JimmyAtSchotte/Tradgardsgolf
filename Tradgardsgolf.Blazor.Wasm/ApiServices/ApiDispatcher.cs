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
        Task Dispatch(IRequest request);
    }

    public class ApiDispatcher(HttpClient httpClient) : IApiDispatcher
    {
        public async Task<TResponse?> Dispatch<TResponse>(IRequest<TResponse> request)
        {
            var dispatchUrl = DispatchUrlBuilder.Build(request);
            var requestBody = JsonSerializer.Serialize(request, request.GetType());
            var response = await httpClient.PostAsync(dispatchUrl, new StringContent(requestBody, Encoding.UTF8, "application/json"));
        
            if(response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<TResponse>()
                       ?? throw new InvalidOperationException("Could not read response message");
            
            throw new DispatchException(response, dispatchUrl, requestBody);
     
        }

        public async Task Dispatch(IRequest request)
        {
            var dispatchUrl = DispatchUrlBuilder.Build(request);
            var requestBody = JsonSerializer.Serialize(request, request.GetType());
            var response = await httpClient.PostAsync(dispatchUrl, new StringContent(requestBody, Encoding.UTF8, "application/json"));
            
            if(response.IsSuccessStatusCode)
                return;
            
            throw new DispatchException(response, dispatchUrl, requestBody);
        }
    }

    public class DispatchException : Exception
    {
        public HttpResponseMessage Response { get; }
        public string Url { get; }        
        public string Body { get; }

        public DispatchException(HttpResponseMessage response, string url, string body) : base()
        {
            Response = response;
            Url = url;
            Body = body;
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
        
        public static string Build(IRequest request)
        {
            var requestType = request.GetType();
            var domain = requestType.Namespace.Substring(ContractNamespacePrefix.Length + 1);
            
            return $"/api/dispatch/{domain}/{requestType.Name}";
        }
    }
}