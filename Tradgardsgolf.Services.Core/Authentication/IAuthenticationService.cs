namespace Tradgardsgolf.Core.Services.Authentication
{
    public interface IAuthenticationService
    {
        IAuthenticationModelResult AuthenticateWithCredentials(ICredentialsModel credentials);
    }
}
