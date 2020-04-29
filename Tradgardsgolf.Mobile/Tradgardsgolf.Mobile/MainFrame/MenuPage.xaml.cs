using System.Collections.Generic;
using System.ComponentModel;
using Tradgardsgolf.Mobile.Admin;
using Tradgardsgolf.Mobile.Events;
using Tradgardsgolf.Mobile.Models;
using Tradgardsgolf.Mobile.Play;
using Xamarin.Forms;

namespace Tradgardsgolf.Mobile.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        List<IHomeMenuItem> menuItems;

        public MenuPage()
        {
            InitializeComponent();

            menuItems = new List<IHomeMenuItem>
            {
                new HomeMenuItem<Courses>("Spela"),
                new HomeMenuItem<Account>("Ditt konto"),
                new HomeMenuItem<CreateCourse>("Skapa en bana")
            };

            ListViewMenu.ItemsSource = menuItems;
            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += ListViewMenu_ItemSelected;
        }

        private async void ListViewMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var appPageType = ((IHomeMenuItem)e.SelectedItem).AppPageType;
            MessagingCenter.Send(new NavigationEvent(appPageType), nameof(NavigationEvent));
        }
    }
}