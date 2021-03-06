﻿using Tradgardsgolf.Core.Encryption;

namespace Tradgardsgolf.Core.Infrastructure.Authentication
{
    public interface ICredentialsDto
    {
        string Email { get; }
        EncryptedString Password { get; }
    }
}