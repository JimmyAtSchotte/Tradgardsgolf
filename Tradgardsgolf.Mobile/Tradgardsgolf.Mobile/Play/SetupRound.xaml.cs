using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tradgardsgolf.Mobile.Play
{
    public class SetupRoundFactory : BaseAppPageFactory<SetupRound>
    {
        public override Page Create()
        {
            return new SetupRound();
        }
    }    

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SetupRound : ContentPage
    {
        public SetupRound()
        {
            InitializeComponent();
        }
    }
}