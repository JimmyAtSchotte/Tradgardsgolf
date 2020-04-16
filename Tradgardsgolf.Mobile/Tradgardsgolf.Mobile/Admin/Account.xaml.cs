using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tradgardsgolf.Mobile.Admin
{
    public class AccountFactory : BaseAppPageFactory<Account>
    {
        public override Page Create()
        {
            return new Account();
        }
    }


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Account : ContentPage
    {
        public Account()
        {
            InitializeComponent();
        }
    }
}