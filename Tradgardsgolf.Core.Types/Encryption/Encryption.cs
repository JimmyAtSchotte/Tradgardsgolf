namespace Tradgardsgolf.Core.Encryption
{
    public struct Encryption
    {
        public static IEncryption Default => new NoneEncryption();
        public static IEncryption None => new NoneEncryption();

    }
}
