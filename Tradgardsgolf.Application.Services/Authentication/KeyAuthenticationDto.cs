using System;
using Tradgardsgolf.Core.Infrastructure.Authentication;
using Tradgardsgolf.Core.Services.Authentication;

namespace Tradgardsgolf.Services.Authentication
{
    public class KeyAuthenticationDto : IKeyAuthenticationDto
    {
        private readonly ITokenAuthenticationModel _model;

        public int Id => GetId();
        public string Key => GetKey();
        public KeyAuthenticationDto(ITokenAuthenticationModel model)
        {
            _model = model;
        }

        private string GetKey()
        {
            var values = _model.Token.Split(':');

            if (values.Length <= 1)
                return Guid.NewGuid().ToString();

            return values[1];
        }

        private int GetId()
        {
            var values = _model.Token.Split(':');

            if (values.Length <= 1)
                return 0;

            if (int.TryParse(values[0], out var id))
                return id;

            return 0;
        }
    }
}
