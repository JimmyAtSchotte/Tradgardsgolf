namespace Tradgardsgolf.SharedKernel.Encryption
{
    public class NoneEncryption : IEncryption
    {
        public string Encrypt(string input)
        {
            return input;
        }
    }
}
