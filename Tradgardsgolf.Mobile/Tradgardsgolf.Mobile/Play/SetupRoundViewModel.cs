using System;
using System.Collections.Generic;
using System.Text;
using Tradgardsgolf.ApiClient.Course;
using Tradgardsgolf.Mobile.ViewModels;
using Xamarin.Forms;

namespace Tradgardsgolf.Mobile.Play
{
    public class SetupRoundViewModel : BaseViewModel
    {
        public Course Course { get; set; }    

        public SetupRoundViewModel(Course course)
        {
            Course = course;
            Title = $"Välj spelare";
        }
    }
}
