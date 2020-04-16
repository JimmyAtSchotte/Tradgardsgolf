using Grpc.Net.Client;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tradgardsgolf.ApiClient;
using Tradgardsgolf.Mobile.DataStore;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tradgardsgolf.Mobile.Login
{
    public class LoginPageFactory : BaseAppPageFactory<LoginPage>
    {
        private readonly IContext _context;

        public LoginPageFactory(IContext context)
        {
            _context = context;
        }

        public override Page Create()
        {
            return new LoginPage(_context);
        }
    }


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private readonly IContext _context;
        
        public LoginPage(IContext context)
        {
            InitializeComponent();

            _context = context;
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            await _context.Authentication.AuthenticateAsync(Email.Text, Password.Text);
        }
    }
}