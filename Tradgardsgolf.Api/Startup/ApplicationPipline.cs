﻿using AspNetCore.FriendlyExceptions.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Tradgardsgolf.Api.CosmosRUTracking;

namespace Tradgardsgolf.Api.Startup;

public static class ApplicationPipline
{
    public static void ConfigureApplicationPipeline(this WebApplication app, IConfigurationRoot configuration)
    {
        if (app.Environment.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.ConfigureSwaggerUI(configuration);

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors();

        app.UseFriendlyExceptions();

        app.UseAuthentication();
        app.UseAuthorization();
        
        app.UseCosmosRuTracker();

        app.MapControllers();
    }
}