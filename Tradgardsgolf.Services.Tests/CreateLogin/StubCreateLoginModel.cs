using Tradgardsgolf.Core.Services.CreateLogin;

namespace Tradgardsgolf.Tests.Services.CreateLogin
{
    public class StubCreateLoginModel : ICreateLoginModel
    {
        public string Email { get; }
        public string Password { get; }

        public StubCreateLoginModel(string email = "example@example.com", string password = "randompassword")
        {
            Email = email;
            Password = password;
        }
    }
}
