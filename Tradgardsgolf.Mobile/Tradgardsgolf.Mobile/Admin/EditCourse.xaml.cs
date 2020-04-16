using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tradgardsgolf.Mobile.Admin
{
    public class EditCourseFactory : BaseAppPageFactory<EditCourse>
    {
        public override Page Create()
        {
            return new EditCourse();
        }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditCourse : ContentPage
    {
        public EditCourse()
        {
            InitializeComponent();
        }
    }
}