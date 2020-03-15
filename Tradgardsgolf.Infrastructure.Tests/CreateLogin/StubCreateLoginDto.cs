using Tradgardsgolf.Core.Email;
using Tradgardsgolf.Core.Infrastructure.Login;
using Tradgardsgolf.SharedKernel.Encryption;

namespace Tradgardsgolf.Infrastructure.Tests.CreateLogin
{
    public class StubCreateLoginDto : ICreateLoginDto
    {
        public EmailString Email { get; }
        public EncryptedString Password { get; }

        public StubCreateLoginDto(string email = "example@example.com", string password = "Password")
        {
            Email = new EmailString(email);
            Password = new EncryptedString(password);
        }
    }
}