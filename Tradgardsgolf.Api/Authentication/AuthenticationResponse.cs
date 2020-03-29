using System;
using System.Text;
using Tradgardsgolf.Core.Services.Authentication;

namespace Tradgardsgolf.Api.Authentication
{
    public class AuthenticationResponse 
    {
        private readonly IAuthenticationModelResult _modelResult;
        public string Email => _modelResult.Email;
        public string Name => _modelResult.Name;
        public string Token => GetToken();

        public AuthenticationResponse(IAuthenticationModelResult modelResult)
        {
            _modelResult = modelResult;
        }

        private string GetToken()
        {
            var bytes = Encoding.Default.GetBytes($"{_modelResult.Id}:{_modelResult.Key}");

            return Convert.ToBase64String(bytes);
            
        }
    }
}