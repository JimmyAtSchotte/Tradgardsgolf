using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Tradgardsgolf.Mobile.Events;
using Tradgardsgolf.Mobile.Login;
using Tradgardsgolf.Mobile.Play;
using Xamarin.Forms;

namespace Tradgardsgolf.Mobile.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        private readonly IDictionary<Type, NavigationPage> _pageCache;
        private readonly IAppPageStrategy _appPageStrategy;

        public MainPage(IAppPageStrategy appPageStrategy)
        {           
            InitializeComponent();      

            MasterBehavior = MasterBehavior.Popover;

            _pageCache = new Dictionary<Type, NavigationPage>();
            _appPageStrategy = appPageStrategy;

            MessagingCenter.Subscribe<UnauthorizedEvent>(this, nameof(UnauthorizedEvent), async (context) => {  
                await Navigation.PushModalAsync(GetPage(typeof(LoginPage)));
            });

            //MessagingCenter.Subscribe<LoginPage>(this, MessageCommands.AUTHORIZED, async (obj) => {
            //    await Navigation.PopModalAsync();
            //});

            //Detail = GetPage(typeof(Courses));
        }

        public async Task NavigateFromMenu(Type pageType)
        {
            await NavigateTo(GetPage(pageType));
        }

        private NavigationPage GetPage(Type pageType)
        {
            if (_pageCache.TryGetValue(pageType, out var displayPage))
                return displayPage;

            var page = _appPageStrategy.Create(pageType);

            displayPage = new NavigationPage(page);
            _pageCache.Add(pageType, displayPage);

            return displayPage;
        }

        private async Task NavigateTo(NavigationPage page)
        {
            if (page == Detail)
                return;

            Detail = page;

            if (Device.RuntimePlatform == Device.Android)
                await Task.Delay(100);

            IsPresented = false;
        }
    }
}