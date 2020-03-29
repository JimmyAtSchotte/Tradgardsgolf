using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tradgardsgolf.Mobile.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

       
        private void Login_Clicked(object sender, EventArgs e)
        {
            //using var channel = GrpcChannel.ForAddress("https://10.0.2.2:5001");
            //var client = new Tradgardsgolf.Grpc.Protos.Greeter.GreeterClient(channel);

            //var response = client.SayHello(new Grpc.Protos.HelloRequest());
        }
    }
}