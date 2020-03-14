using Tradgardsgolf.Core.Services.CreateLogin;
using Tradgardsgolf.Core.SharedKernel.Enums;

namespace Tradgardsgolf.CreateLogin
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
