namespace Tradgardsgolf.Core.Encryption
{
    public class NoneEncryption : IEncryption
    {
        public string Encrypt(string input)
        {
            return input;
        }
    }
}
