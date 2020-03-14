namespace Tradgardsgolf.Core.Services.Authentication
{
    public interface ICredentialsModel
    {
        string Email { get; }
        string Password { get; }
    }
}
