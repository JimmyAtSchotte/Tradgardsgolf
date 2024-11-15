using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tradgardsgolf.Api.CosmosRUTracking;

[SuppressMessage("ReSharper", "UnusedMethodReturnValue.Global")]
public static class DbContextOptionsBuilderExtensions
{
    public static DbContextOptionsBuilder EnableCosmosRuTracking(this DbContextOptionsBuilder builder, IServiceProvider services)
    {
        var ruTracker = services.GetService<CosmosRUTracker>();
        var httpContextAccessor = services.GetService<IHttpContextAccessor>();
        
        builder.EnableSensitiveDataLogging();
        builder.LogTo(log =>
        {
            ruTracker.Log(log, httpContextAccessor.HttpContext?.TraceIdentifier ?? "UNKNOWN");
        });

        return builder;
    }
    
}