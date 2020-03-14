using Tradgardsgolf.Core.Services.Authentication;

namespace Tradgardsgolf.Tests.Authentication
{
    public class StubCredentialsModel : ICredentialsModel
    {
        public string Email { get; }
        public string Password { get; }

        public StubCredentialsModel(string email = "exampel@example.com", string password = "password!")
        {
            Email = email;
            Password = password;
        }
    }
}
