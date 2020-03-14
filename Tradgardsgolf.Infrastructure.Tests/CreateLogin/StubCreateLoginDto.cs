using Tradgardsgolf.Core.Infrastructure.Login;

namespace Tradgardsgolf.Tests.Infrastructure.CreateLogin
{
    public class StubCreateLoginDto : ICreateLoginDto
    {
        public string Email { get; }
        public string Password { get; }

        public StubCreateLoginDto(string email = "example@example.com", string password = "Password")
        {
            Email = email;
            Password = password;
        }
    }
}