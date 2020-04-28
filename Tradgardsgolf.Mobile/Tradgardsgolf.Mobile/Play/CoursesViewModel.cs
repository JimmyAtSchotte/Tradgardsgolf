using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Tradgardsgolf.ApiClient;
using Tradgardsgolf.Mobile.ViewModels;
using Xamarin.Forms;
using Tradgardsgolf.ApiClient.Authentication;

namespace Tradgardsgolf.Mobile.Play
{
    public class CoursesViewModel : BaseViewModel
    {
        private readonly TradgradsgolfApiClient _apiClient;

        public ObservableCollection<string> Courses { get; set; }
        public Command LoadCoursesCommand { get; private set; }


        public CoursesViewModel(TradgradsgolfApiClient apiClient)
        {
            _apiClient = apiClient;

            Courses = new ObservableCollection<string>();
            LoadCoursesCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Courses.Clear();
                var items = await _apiClient.IsAuthorizedAsync();
                //foreach (var item in items)
                //{
                //    Items.Add(item);
                //}
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
