using Tradgardsgolf.SharedKernel.Encryption;

namespace Tradgardsgolf.Core.Infrastructure.Login
{
    public interface ICreateLoginDto
    {
        string Email { get; }
        EncryptedString Password { get; }
    }
}
