using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tradgardsgolf.ApiClient.Course;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tradgardsgolf.Mobile.Play
{    
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SetupRound : ContentPage
    {
        private readonly Course _course;
               
        public SetupRound(Course course)
        {
            InitializeComponent();

            _course = course;
        }
    }
}