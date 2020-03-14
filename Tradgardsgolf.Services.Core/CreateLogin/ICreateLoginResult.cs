using Tradgardsgolf.Core.SharedKernel.Enums;

namespace Tradgardsgolf.Core.Services.CreateLogin
{
    public interface ICreateLoginResult
    {
        CreateLoginStatus Status { get; }
    }
}
