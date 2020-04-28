using System.Net;

namespace Tradgardsgolf.ApiClient
{
    public interface IResponse
    {
        HttpStatusCode StatusCode { get; }
        string Message { get; }
    }
    public interface IResponse<T> : IResponse
    {
        T Result { get; }
    }
}
