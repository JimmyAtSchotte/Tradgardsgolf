using Tradgardsgolf.Mobile.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Tradgardsgolf.Mobile
{
    public partial class App : Application
    {     
        public static string ApiUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:58816" : "http://localhost:58816";

        public App(MainPage mainPage)
        {
            InitializeComponent();

            MainPage = mainPage;
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
