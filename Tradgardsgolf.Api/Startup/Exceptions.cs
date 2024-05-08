using System.Net;
using AspNetCore.FriendlyExceptions.Extensions;
using AspNetCore.FriendlyExceptions.Transforms;
using Microsoft.AspNetCore.Builder;
using Tradgardsgolf.Core.Exceptions;

namespace Tradgardsgolf.Api.Startup;

public static class Exceptions
{
    public static void ConfigureExceptions(this WebApplicationBuilder builder)
    {
        builder.Services.AddFriendlyExceptionsTransforms(options =>
        {
            options.Transforms = TransformsCollectionBuilder.Begin()
                .Map<UnauthorizedException>().To(HttpStatusCode.Unauthorized, "Unauthorized", ex => "Unauthorized")
                .Map<ForbiddenException>().To(HttpStatusCode.Forbidden, "Forbidden", ex => "Forbidden")
                .MapAllOthers().To(HttpStatusCode.InternalServerError, "Internal Server Error", ex => ex.Message)
                .Done();
        });
    }
}