using System;
using System.Collections.Generic;
using System.Text;

namespace Tradgardsgolf.Core.Infrastructure.Authentication
{
    public interface IAuthenticationRepository
    {
        IAuthenticateDtoResult AuthenticateWithCredentials(ICredentialsDto dto);
    }
}
