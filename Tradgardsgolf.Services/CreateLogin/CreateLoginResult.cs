using Tradgardsgolf.Core.Enums;
using Tradgardsgolf.Core.Services.CreateLogin;

namespace Tradgardsgolf.Services.CreateLogin
{
    public class CreateLoginResult : ICreateLoginResult
    {
        public CreateLoginStatus Status { get; }

        public CreateLoginResult(CreateLoginStatus result)
        {
            Status = result;
        }
    }
}
