using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Tradgardsgolf.Mobile.Models;
using Tradgardsgolf.Mobile.DataStore;

namespace Tradgardsgolf.Mobile.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        private readonly IDictionary<Type, NavigationPage> _pageCache;
        private readonly IAppPageStrategy _appPageStrategy;

        public MainPage(IContext context, IAppPageStrategy appPageStrategy)
        {
            _appPageStrategy = appPageStrategy;
            context.Unauthorized += OnUnauthorized;

            InitializeComponent();      
            MasterBehavior = MasterBehavior.Popover;

            _pageCache = new Dictionary<Type, NavigationPage>();
        }

        private async Task OnUnauthorized()
        {
            //Navigation.PushModalAsync()
        }

        public async Task NavigateFromMenu(Type type)
        {
            if(!_pageCache.TryGetValue(type, out var displayPage))
            {
                var page = _appPageStrategy.Create(type);
                if (page == null)
                    return;

                displayPage = new NavigationPage(page);
                _pageCache.Add(type, displayPage);
            }

            if (displayPage == Detail)
                return;

            Detail = displayPage;

            if (Device.RuntimePlatform == Device.Android)
                await Task.Delay(100);

            IsPresented = false;
        }
    }
}