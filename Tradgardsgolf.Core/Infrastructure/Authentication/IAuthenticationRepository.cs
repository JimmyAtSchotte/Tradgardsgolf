using System;
using System.Collections.Generic;
using System.Text;
using Ardalis.Specification;

namespace Tradgardsgolf.Core.Infrastructure.Authentication
{
    public interface IAuthenticationRepository
    {
        IAuthenticateDtoResult CredentialsAuthentication(ICredentialsDto dto);
        IAuthenticateDtoResult KeyAuthentication(IKeyAuthenticationDto dto);
    }

    public interface IKeyAuthenticationDto
    {
        public int Id { get; }
        public string Key { get; }
    }
}
