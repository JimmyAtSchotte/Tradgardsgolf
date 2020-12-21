using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Tradgardsgolf.Blazor.Wasm.Components.Forms
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
