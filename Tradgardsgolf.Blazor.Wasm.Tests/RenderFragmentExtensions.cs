using Bunit;
using Microsoft.AspNetCore.Components;

namespace Tradgardsgolf.Blazor.Wasm.Tests.Pages;

public static class RenderFragmentExtensions
{
    public static IRenderedComponent<T>? FindComponent<T>(this IRenderedFragment fragment,
        Func<IRenderedComponent<T>, bool> func)
        where T : IComponent
    {
        return fragment
            .FindComponents<T>()
            .FirstOrDefault(func.Invoke);
    }
}