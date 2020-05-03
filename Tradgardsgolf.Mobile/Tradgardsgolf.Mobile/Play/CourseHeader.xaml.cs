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
        public static readonly BindableProperty CourseProperty = BindableProperty.Create("Course", typeof(Course), typeof(CourseHeader), null);

        public Course Course
        {
            get => (Course)GetValue(CourseProperty);
            set => SetValue(CourseProperty, value);
        }

        public CourseHeader()
        {
            InitializeComponent();

            CourseName.SetBinding(Label.TextProperty, new Binding(nameof(Course.Name), source: Course));
        }
    }
}