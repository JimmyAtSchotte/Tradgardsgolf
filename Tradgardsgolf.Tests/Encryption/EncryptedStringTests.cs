using NUnit.Framework;
using System;
using Tradgardsgolf.Core.Encryption;

namespace Tradgardsgolf.SharedKernel.Tests.Encryption
{
    [TestFixture]
    public class EncryptedStringTests
    {
        [Test]
        public void DefauktEncryotionShouldBeNone()
        {
            Assert.IsInstanceOf<NoneEncryption>(Core.Encryption.Encryption.Default);
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
            var encrypted = new EncryptedString("test", Core.Encryption.Encryption.None);

            Assert.AreEqual("test", encrypted.Value);
        }
    }
}
