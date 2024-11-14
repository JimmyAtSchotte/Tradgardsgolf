using Bunit;
using Microsoft.AspNetCore.Components;

namespace Tradgardsgolf.Blazor.Wasm.Tests.__Extensions;

public static class IRenderedFragmentExtensions
{
    public static IRenderedComponent<T> RequireComponent<T>(this IRenderedFragment fragment, Func<IRenderedComponent<T>, bool> predicate) 
        where T : IComponent {
        
        var component = fragment.FindComponent(predicate);
        
        if(component is null)
            throw new Exception(" Required component is null");

        return component;
    }
    
}