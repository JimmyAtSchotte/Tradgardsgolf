using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tradgardsgolf.Mobile.Play
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SetupRound : ContentPage
    {
        private readonly SetupRoundViewModel _setupRoundViewModel;
        
        public SetupRound(SetupRoundViewModel setupRoundViewModel)
        {
            InitializeComponent();

            BindingContext = _setupRoundViewModel = setupRoundViewModel;
        }
    }
}