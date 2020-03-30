using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tradgardsgolf.Mobile.Services;
using Tradgardsgolf.Mobile.Views;
using Tradgardsgolf.Mobile.Login;

namespace Tradgardsgolf.Mobile
{
    public partial class App : Application
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        //To debug on Android emulators run the web backend against .NET Core not IIS
        //If using other emulators besides stock Google images you may need to adjust the IP address
        public static string AzureBackendUrl =  DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5000" : "http://localhost:5000";
        public static bool UseMockDataStore = true;

        public static string ApiUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:58816" : "http://localhost:58816";


        public App()
        {
            InitializeComponent();
            
            DependencyService.Register<IApiRepository, ApiRepository>();

            if (UseMockDataStore)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<AzureDataStore>();

            MainPage = new MainPage();
        }



        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
