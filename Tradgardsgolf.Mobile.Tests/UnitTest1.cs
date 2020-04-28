using NUnit.Framework;
using Tradgardsgolf.ApiClient;

namespace Tradgardsgolf.Mobile.Tests
{
    public class StartupTest
    {

        [Test]
        public void GetAppPageStrategy()
        {
            Startup.CreateApplication().GetService<IAppPageStrategy>();
        }

        [Test]
        public void GetAppPageFactories()
        {
            Startup.CreateApplication().GetServices<IAppPageFactory>();
        }

        [Test]
        public void GetTradgradsgolfApiClient()
        {
            Startup.CreateApplication().GetService<TradgradsgolfApiClient>();
        }
    }
}