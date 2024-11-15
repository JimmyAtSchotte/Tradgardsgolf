using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Tradgardsgolf.BlazorWasm;

public class AppendBearerAuthorizationMessageHandler : DelegatingHandler, IDisposable
{
    private readonly AuthenticationStateChangedHandler? _authenticationStateChangedHandler;
    private readonly IAccessTokenProvider _provider;
    private AccessToken? _lastToken;

    public AppendBearerAuthorizationMessageHandler(IAccessTokenProvider provider)
    {
        _provider = provider;

        if (_provider is not AuthenticationStateProvider authStateProvider) 
            return;
        
        _authenticationStateChangedHandler = _ => { _lastToken = null; };
        authStateProvider.AuthenticationStateChanged += _authenticationStateChangedHandler;
    }

    void IDisposable.Dispose()
    {
        if (_provider is AuthenticationStateProvider authStateProvider)
            authStateProvider.AuthenticationStateChanged -= _authenticationStateChangedHandler;
        Dispose(true);
        GC.SuppressFinalize(this);
    }


    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var now = DateTimeOffset.Now;

        if (_lastToken == null || now >= _lastToken.Expires.AddMinutes(-5))
        {
            var tokenResult = await _provider.RequestAccessToken();

            Console.WriteLine($"tokenResult.Status: {tokenResult.Status}");

            if (tokenResult.TryGetToken(out var token))
                _lastToken = token;
        }

        if (_lastToken is not null)
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _lastToken.Value);


        return await base.SendAsync(request, cancellationToken);
    }
}