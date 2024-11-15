using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Tradgardsgolf.Api.ActionFilters;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class ImageReferenceFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context) { }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result is not JsonResult response)
            return;

        ImageReferenceMutator.Mutate(context.HttpContext, response.Value);
    }
}