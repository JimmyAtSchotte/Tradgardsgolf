using Tradgardsgolf.Mobile.MainFrame;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Tradgardsgolf.Mobile
{
    public partial class App : Application
    {   
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
