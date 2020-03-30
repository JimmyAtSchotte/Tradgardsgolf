using Grpc.Net.Client;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tradgardsgolf.ApiClient;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tradgardsgolf.Mobile.Login
{
    public class StubServiceClientCredentials : ServiceClientCredentials
    {

    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private IApiRepository Api => DependencyService.Get<IApiRepository>();

        public LoginPage()
        {
            InitializeComponent();
        }
       
        private async void Login_Clicked(object sender, EventArgs e)
        {
            var credentials = new ApiClient.Models.CredentialsModel(Email.Text, Password.Text);
            if (await Api.AuthenticateAsync(credentials))
                ;
           
        }
    }
}