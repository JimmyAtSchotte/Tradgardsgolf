namespace Tradgardsgolf.Core.Encryption
{

    public class EncryptedString
    {
        private readonly string _value;
        private readonly IEncryption _encryption;

        public string Value => _encryption.Encrypt(_value);

        public EncryptedString(string value, IEncryption encryption = null)
        {
            _value = value;
            _encryption = encryption ?? Encryption.Default;
        }
    }
}
