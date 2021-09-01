namespace Tradgardsgolf.Core.Services.Authentication
{
    public interface IAuthenticationService
    {
        IAuthenticationModelResult CredentialsAuthentication(ICredentialsModel model);
        IAuthenticationModelResult TokenAuthentication(ITokenAuthenticationModel model);
    }

    public interface ITokenAuthenticationModel
    {
        string Token { get; }
    }
}
