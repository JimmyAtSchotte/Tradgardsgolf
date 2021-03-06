﻿using Tradgardsgolf.Core.Encryption;
using Tradgardsgolf.Core.Infrastructure.Authentication;

namespace Tradgardsgolf.Infrastructure.Tests.Authentication
{
    public class StubCredentialsDto : ICredentialsDto
    { 
        public string Email { get; }
        public EncryptedString Password { get; }

        public StubCredentialsDto(string email = "example@example.com", string password = "Password1")
        {
            Email = email;
            Password = new EncryptedString(password);
        }
    }
}
