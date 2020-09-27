using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Tradgardsgolf.ApiClient;
using Tradgardsgolf.Mobile.ViewModels;
using Xamarin.Forms;
using Tradgardsgolf.ApiClient.Course;

namespace Tradgardsgolf.Mobile.Play
{
    public class CoursesViewModel : BaseViewModel
    {
        private readonly TradgradsgolfApiClient _apiClient;

        public ObservableCollection<Course> Courses { get; set; }
        public Command LoadCoursesCommand { get; private set; }
  
        public CoursesViewModel(TradgradsgolfApiClient apiClient)
        {
            _apiClient = apiClient;

            Courses = new ObservableCollection<Course>();
            LoadCoursesCommand = new Command(async () => await ExecuteLoadItemsCommand());
            Title = "Spela";
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Courses.Clear();
                var response = await _apiClient.ListAllCourses();

                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var courses = response.Result;

                    foreach (var course in courses)
                        Courses.Add(course);
                }
                                
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
