using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Tradgardsgolf.Api.ActionFilters;

namespace Tradgardsgolf.Api;

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