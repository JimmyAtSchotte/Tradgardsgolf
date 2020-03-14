using Tradgardsgolf.Core.Infrastructure.Authentication;

namespace Tradgardsgolf.Core.Services.Authentication
{
    public interface IAuthenticationService
    {
        IAuthenticationAdapter AuthenticateWithCredentials(ICredentialsModel credentials);
    }
}
