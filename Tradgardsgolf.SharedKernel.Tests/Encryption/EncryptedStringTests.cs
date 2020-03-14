using NUnit.Framework;
using System;
using Tradgardsgolf.SharedKernel.Encryption;

namespace Tradgardsgolf.SharedKernel.Tests.Encryption
{
    [TestFixture]
    public class EncryptedStringTests
    {
        [Test]
        public void DefauktEncryotionShouldBeNone()
        {
            Assert.IsInstanceOf<SharedKernel.Encryption.NoneEncryption>(SharedKernel.Encryption.Encryption.Default);
        }

        [Test]
        public void ShouldThrowNotImplentedWhenDefaultEncryption()
        {
            var encrypted = new EncryptedString("test", new DefaultEncryption());
            
            Assert.Throws<NotImplementedException>(() => { var result = encrypted.Value; });
        }

        [Test]
        public void ShouldNotEncryptWithNoneEncryption()
        {
            var encrypted = new EncryptedString("test", SharedKernel.Encryption.Encryption.None);

            Assert.AreEqual("test", encrypted.Value);
        }
    }
}
