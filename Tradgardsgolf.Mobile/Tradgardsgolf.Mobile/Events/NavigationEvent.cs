using System;
using System.Collections.Generic;
using System.Text;

namespace Tradgardsgolf.Mobile.Events
{
    public class NavigationEvent
    {
        public Type AppPageType { get; }

        public NavigationEvent(Type appPageType)
        {
            AppPageType = appPageType;
        }

    }
}
