using System;
using System.Collections.Generic;
using System.Text;

namespace Tradgardsgolf.Mobile.Models
{
    public interface IHomeMenuItem
    {
        Type AppPageType { get; }

        string Title { get; }
    }

    public class HomeMenuItem<TAppPage> : IHomeMenuItem
    {
        public string Title { get; }
        public Type AppPageType => typeof(TAppPage);

        public HomeMenuItem(string title)
        {
            Title = title;
        }
    }


}
