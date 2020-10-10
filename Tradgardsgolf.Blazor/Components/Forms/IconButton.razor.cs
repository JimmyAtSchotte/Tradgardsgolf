using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tradgardsgolf.Blazor.Components.Forms
{
    public class IconButtonBase : ComponentBase
    {
        [Parameter]
        public Action OnClick { get; set; }

        [Parameter]
        public string Icon { get; set; }

        protected async Task InvokeOnClick()
        {
            OnClick?.Invoke();
        }
    }
}
