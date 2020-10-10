using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tradgardsgolf.Blazor.Components.Forms
{
    public class ImageButtonBase : ComponentBase
    {
        [Parameter]
        public Action OnClick { get; set; }

        [Parameter]
        public string Text { get; set; }

        [Parameter]
        public string Icon { get; set; }


        [Parameter]
        public bool Disabled { get; set; }

        public ImageButtonBase()
        {
            Disabled = false;
        }

        protected async Task InvokeOnClick()
        {
            OnClick?.Invoke();
        }
    }
}
