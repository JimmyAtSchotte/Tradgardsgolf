﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tradgardsgolf.Mobile.Play
{
    public class CoursesFactory : BaseAppPageFactory<Courses>
    {
        public override Page Create()
        {
            return new Courses();
        }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Courses : ContentPage
    {
        public Courses()
        {
            InitializeComponent();
        }
    }
}