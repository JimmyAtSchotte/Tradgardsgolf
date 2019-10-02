using Tradgardsgolf.Core.Interfaces.Adapters;
using Tradgardsgolf.Core.Interfaces.Models;

namespace Tradgardsgolf.Core.Interfaces
{
    public interface IAuthenticationService
    {
        IAuthenticationAdapter AuthenticateWithCredentials(ICredentialsModel credentials);
    }
}
