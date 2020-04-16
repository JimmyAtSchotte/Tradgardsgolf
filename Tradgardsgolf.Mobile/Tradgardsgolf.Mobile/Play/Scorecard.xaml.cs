using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tradgardsgolf.Mobile.Play
{
    public class ScorecardFactory : BaseAppPageFactory<Scorecard>
    {
        public override Page Create()
        {
            return new Scorecard();
        }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Scorecard : ContentPage
    {
        public Scorecard()
        {
            InitializeComponent();
        }
    }
}