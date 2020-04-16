using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tradgardsgolf.Mobile.Admin
{
    public class CreateCourseFactory : BaseAppPageFactory<CreateCourse>
    {
        public override Page Create()
        {
            return new CreateCourse();
        }
    }



    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateCourse : ContentPage
    {
        public CreateCourse()
        {
            InitializeComponent();
        }
    }
}