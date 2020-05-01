using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Tradgardsgolf.Mobile.Events;
using Tradgardsgolf.Mobile.Login;
using Tradgardsgolf.Mobile.Play;
using Xamarin.Forms;

namespace Tradgardsgolf.Mobile.MainFrame
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
            
            MessagingCenter.Subscribe<UnauthorizedEvent>(this, nameof(UnauthorizedEvent), OnUnauthorizedEvent);
            MessagingCenter.Subscribe<NavigationEvent>(this, nameof(NavigationEvent), OnNavigationEvent);
            MessagingCenter.Subscribe<AuthorizedEvent>(this, nameof(AuthorizedEvent), OnAuthorizedEvent);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if((Detail as NavigationPage)?.CurrentPage is BootUpPage)
                Task.Factory.StartNew(async () => await NavigateTo(GetPage(typeof(Courses))));
        }

        private async void OnUnauthorizedEvent(UnauthorizedEvent unauthorizedEvent) => await Navigation.PushModalAsync(GetPage(typeof(LoginPage)));
        private async void OnAuthorizedEvent(AuthorizedEvent authorizedEvent) => await Navigation.PopModalAsync();
        private async void OnNavigationEvent(NavigationEvent navigationEvent) => await NavigateTo(GetPage(navigationEvent.AppPageType));              

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