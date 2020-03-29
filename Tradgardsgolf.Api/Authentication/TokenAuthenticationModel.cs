using Tradgardsgolf.Core.Services.Authentication;

namespace Tradgardsgolf.Api.Authentication
{
    public class TokenAuthenticationModel : ITokenAuthenticationModel
    {        
        public string Token { get; }

        public TokenAuthenticationModel(string authentication)
        {
            Token = authentication;
        }
    }
}
