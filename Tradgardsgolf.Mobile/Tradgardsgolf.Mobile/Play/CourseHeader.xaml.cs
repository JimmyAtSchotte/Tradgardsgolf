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
    public partial class CourseHeader : ContentView
    {
        public static readonly BindableProperty CourseProperty = BindableProperty.Create(nameof(Course), typeof(Course), typeof(CourseHeader), default);

        public Course Course
        {
            get => GetValue(CourseProperty) as Course;                
            set => SetValue(CourseProperty, value);
        }

        public CourseHeader()
        {
            InitializeComponent();

            CourseName.SetBinding(Label.TextProperty, new Binding(nameof(Course.Name), source: Course));
        }
    }
}