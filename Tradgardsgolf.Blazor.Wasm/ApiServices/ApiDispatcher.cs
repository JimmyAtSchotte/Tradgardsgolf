﻿using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Contracts;

namespace Tradgardsgolf.BlazorWasm.ApiServices
{
    public interface IApiDispatcher
    {
        Task<TResponse?> Dispatch<TResponse>(IRequest<TResponse> request);
        Task Dispatch(IRequest request);
        Task FileUpload(string filename, byte[] bytes);
        Task FileDelete(string filename);
    }

    public class ApiDispatcher(HttpClient httpClient) : IApiDispatcher
    {
        public async Task<TResponse?> Dispatch<TResponse>(IRequest<TResponse> request)
        {
            var dispatchUrl = DispatchUrlBuilder.Build(request);
            var requestBody = JsonSerializer.Serialize(request, request.GetType());
            var response = await httpClient.PostAsync(dispatchUrl,
                new StringContent(requestBody, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<TResponse>()
                       ?? throw new InvalidOperationException("Could not read response message");

            throw new DispatchException(response, dispatchUrl, requestBody);

        }

        public async Task Dispatch(IRequest request)
        {
            var dispatchUrl = DispatchUrlBuilder.Build(request);
            var requestBody = JsonSerializer.Serialize(request, request.GetType());
            var response = await httpClient.PostAsync(dispatchUrl,
                new StringContent(requestBody, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
                return;

            throw new DispatchException(response, dispatchUrl, requestBody);
        }

        public async Task FileUpload(string filename, byte[] bytes)
        {
            using var content = new MultipartFormDataContent();
            using var fileStream = new MemoryStream(bytes);

            content.Add(new StreamContent(fileStream), "files", filename);
            var response = await httpClient.PostAsync("api/file", content);

            response.EnsureSuccessStatusCode();
        }

        public async Task FileDelete(string filename)
        {
            var response = await httpClient.DeleteAsync($"api/file/{filename}");
            response.EnsureSuccessStatusCode();
        }
    }

    public class DispatchException(HttpResponseMessage response, string url, string body) : Exception
    {
        public HttpResponseMessage Response { get; } = response;
        public string Url { get; } = url;
        public string Body { get; } = body;
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