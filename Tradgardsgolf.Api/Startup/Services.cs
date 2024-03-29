﻿using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tradgardsgolf.Api.Authentication;
using Tradgardsgolf.Api.RequestHandling;
using Tradgardsgolf.Core.Auth;
using Tradgardsgolf.Core.Config;
using Tradgardsgolf.Infrastructure.Database;
using Tradgardsgolf.Infrastructure.Files;

namespace Tradgardsgolf.Api.Startup;

public static class Services
{
    public static void ConfigureServices(this WebApplicationBuilder builder, IConfigurationRoot configuration)
    {
        builder.Services.AddControllers(options =>
        {
            options.Filters.Add<ImageReferenceFilter>();
        });

        builder.Services.AddMediatR(mediatr =>
        {
            mediatr.RegisterServicesFromAssembly(typeof(IRequestHandlingNamespaceMarker).Assembly);
        });
            
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        });
            
        builder.Services.AddLogging();
        builder.Services.AddApplicationInsightsTelemetry();
            
        builder.Services.AddOptions<AllowPlayDistance>().Bind(configuration.GetSection("AllowPlayDistance"));
        builder.Services.AddOptions<AzureStorageOptions>().Bind(configuration.GetSection("AzureStorage"));
         
        builder.Services.AddDbContext<TradgardsgolfContext>(dbContextOptionsBuilder =>
        {
            var connectionString = configuration.GetConnectionString("Database");

            if (string.IsNullOrEmpty(connectionString))
                dbContextOptionsBuilder.UseInMemoryDatabase("Tradgardsgolf");
            else
            {
                dbContextOptionsBuilder.UseSqlServer(connectionString,
                    options =>
                    {
                        options.EnableRetryOnFailure(10);
                    });
            }
        });
        
        builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
    }
}