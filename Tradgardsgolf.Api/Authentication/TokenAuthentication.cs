using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Tradgardsgolf.Api.Authentication
{
    public class TokenAuthentication : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly Core.Services.Authentication.IAuthenticationService _authenticationService;

        public TokenAuthentication(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, Core.Services.Authentication.IAuthenticationService authenticationService) : base(options, logger, encoder, clock)
        {
            _authenticationService = authenticationService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!AuthenticationHeaderValue.TryParse(Request.Headers["Authorization"], out var authenticationHeaderValue))
                return AuthenticateResult.Fail("Invalid authorization header");

            var bytes = Convert.FromBase64String(authenticationHeaderValue.Parameter);
            var authentication = Encoding.Default.GetString(bytes);

            var result = _authenticationService.TokenAuthentication(new TokenAuthenticationModel(authentication));

            if(result.Status != Core.Enums.AuthenticationStatus.Success)
                return AuthenticateResult.Fail("Invalid authorization token");

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, result.Id.ToString())
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);

            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return await Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
