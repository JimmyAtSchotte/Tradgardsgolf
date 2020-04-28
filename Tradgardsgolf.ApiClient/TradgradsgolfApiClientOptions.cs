using System;
using System.Net.Http;

namespace Tradgardsgolf.ApiClient
{
    public class TradgradsgolfApiClientOptions
    {
        public string Url { get; set; }
        public TimeSpan? Timeout { get; set; }
        public Action<HttpClientHandler> ClientHandlerConfiguration { get; set; }
        public Action OnUnathorized { get; set; }
    }
}
