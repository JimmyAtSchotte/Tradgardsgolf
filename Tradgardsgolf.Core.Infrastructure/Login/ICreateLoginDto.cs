using Tradgardsgolf.Core.Email;
using Tradgardsgolf.Core.Encryption;

namespace Tradgardsgolf.Core.Infrastructure.Login
{
    public interface ICreateLoginDto
    {
        EmailString Email { get; }
        EncryptedString Password { get; }
    }
}
