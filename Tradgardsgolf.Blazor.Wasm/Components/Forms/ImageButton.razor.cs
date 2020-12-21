using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Tradgardsgolf.Blazor.Wasm.Components.Forms
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
