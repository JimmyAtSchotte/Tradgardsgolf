using Tradgardsgolf.Mobile.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tradgardsgolf.Mobile.Play;
using Tradgardsgolf.Mobile.Admin;

namespace Tradgardsgolf.Mobile.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
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
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var appPageType = ((IHomeMenuItem)e.SelectedItem).AppPageType;
                await RootPage.NavigateFromMenu(appPageType);
            };
        }
    }
}