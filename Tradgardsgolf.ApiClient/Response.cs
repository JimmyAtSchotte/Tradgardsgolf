using System.Net;

namespace Tradgardsgolf.ApiClient
{
    internal class Response : IResponse
    {
        public HttpStatusCode StatusCode { get; }
        public string Message { get; }

        internal Response(HttpStatusCode statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }  
    }

    internal class Response<T> : Response, IResponse<T>
    {
        public T Result { get; }
        internal Response(T result, HttpStatusCode statusCode, string message) : base(statusCode, message)
        {
            Result = result;
        }
    }
}
