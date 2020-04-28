using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinTemplate.Services;
using XamarinTemplate.Views;

namespace XamarinTemplate
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
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
