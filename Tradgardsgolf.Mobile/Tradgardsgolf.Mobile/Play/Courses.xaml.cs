using Tradgardsgolf.ApiClient;
using Tradgardsgolf.ApiClient.Course;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tradgardsgolf.Mobile.Play
{
    public class CoursesFactory : BaseAppPageFactory<Courses>
    {
        private readonly TradgradsgolfApiClient _apiClient;

        public CoursesFactory(TradgradsgolfApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public override Page Create()
        {
            return new Courses(_apiClient);
        }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Courses : ContentPage
    {
        private readonly CoursesViewModel _coursesViewModel;

        public Courses(TradgradsgolfApiClient apiClient)
        {
            InitializeComponent();
            
            BindingContext = _coursesViewModel = new CoursesViewModel(apiClient);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_coursesViewModel.Courses.Count == 0)
                _coursesViewModel.LoadCoursesCommand.Execute(null);
        }

        private async void Play_Clicked(object sender, System.EventArgs e)
        {
            var playButton = sender as Button;
            var setupRoundViewModel = new SetupRoundViewModel(playButton.BindingContext as Course);
            var setupRound = new SetupRound(setupRoundViewModel);

            await Navigation.PushAsync(setupRound);
        }
    }
}