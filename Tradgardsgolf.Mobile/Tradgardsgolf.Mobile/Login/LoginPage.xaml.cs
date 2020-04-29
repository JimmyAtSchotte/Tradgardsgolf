﻿using System;
using Tradgardsgolf.ApiClient;
using Tradgardsgolf.ApiClient.Authentication;
using Tradgardsgolf.Mobile.Events;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tradgardsgolf.Mobile.Login
{
    public class LoginPageFactory : BaseAppPageFactory<LoginPage>
    {
        private readonly TradgradsgolfApiClient _apiClient;

        public LoginPageFactory(TradgradsgolfApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public override Page Create()
        {
            return new LoginPage(_apiClient);
        }
    }


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private readonly TradgradsgolfApiClient _apiClient;

        public LoginPage(TradgradsgolfApiClient apiClient)
        {
            InitializeComponent();

            _apiClient = apiClient;
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            var response = await _apiClient.AuthenticateAsync(new CredentialsModel(Email.Text, Password.Text));

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                MessagingCenter.Send(new AuthorizedEvent(), nameof(AuthorizedEvent));
        }
    }
}