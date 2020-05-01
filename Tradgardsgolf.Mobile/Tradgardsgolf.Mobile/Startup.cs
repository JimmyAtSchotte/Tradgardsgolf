using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using Tradgardsgolf.ApiClient;
using Tradgardsgolf.Mobile.Events;
using Tradgardsgolf.Mobile.MainFrame;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Tradgardsgolf.Mobile
{
    public class Startup
    {
        private readonly IHost _host;

        private Startup(IHost host)
        {
            _host = host;
        }

        public App GetApp()
        {
            return _host.Services.GetService<App>();
        }

        public T GetService<T>()
        {
            return _host.Services.GetService<T>();
        }

        public IEnumerable<T> GetServices<T>()
        {
            return _host.Services.GetServices<T>();
        }

        public static Startup CreateApplication()
        {
            var root = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "root");
            Directory.CreateDirectory(root);

            var host = new HostBuilder()
                .UseContentRoot(root)
                .ConfigureAppConfiguration(Configuration)
                .ConfigureServices(ConfigureServices)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(ConfigureContainer)
                .Build();

            return new Startup(host);
        }

        private static void Configuration(HostBuilderContext hostContext, IConfigurationBuilder configuration)
        {       
            configuration.AddInMemoryCollection(new Dictionary<string, string>() {               
                { "ApiUrl", DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:58816/api" : "http://localhost:58816/api" } 
            });
        }

        private static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
        {
            services.AddSingleton<App>();
            services.AddTransient<MainPage>();
            services.AddTradgradsgolfApiClient(options => {
                options.Url = hostContext.Configuration.GetValue<string>("ApiUrl");
                options.OnUnathorized = () => MessagingCenter.Send(new UnauthorizedEvent(), nameof(UnauthorizedEvent));
            });
        }

        private static void ConfigureContainer(HostBuilderContext hostContext, ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(App).Assembly).AsImplementedInterfaces();
        }
    }
}
