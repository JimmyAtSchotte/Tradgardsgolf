using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Tradgardsgolf.Api.Startup;

public static class Swagger
{
    private static readonly IDictionary<string, string> Scopes = new Dictionary<string, string>
    {
        { "openid", "" },
        { "offline_access", "" },
        { "https://tradgardsgolf.onmicrosoft.com/api/access", "Grants access to API" }
    };

    public static void ConfigureSwaggerGen(this WebApplicationBuilder builder, IConfigurationRoot configuration)
    {
        var securityScheme = CreateSecurityScheme(configuration);
        var securityRequirement = CreateSecurityRequirement(securityScheme);

        builder.Services.AddSingleton(new SecurityRequirementsOperationFilter(securitySchemaName: securityScheme.Name));

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tradgardsgolf.Api", Version = "v1" });
            c.AddSecurityRequirement(securityRequirement);
            c.AddSecurityDefinition(securityScheme.Name, securityScheme);
            c.OperationFilter<SecurityRequirementsOperationFilter>();
        });
    }

    public static void ConfigureSwaggerUI(this WebApplication app, IConfigurationRoot configuration)
    {
        var clientId = configuration.GetValue<string>("AzureAdB2C:ClientId");

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tradgardsgolf.Api v1");
            c.RoutePrefix = string.Empty;
            c.OAuthClientId(clientId);
            c.OAuthScopes(Scopes.Keys.ToArray());
        });
    }

    private static OpenApiSecurityScheme CreateSecurityScheme(IConfigurationRoot configuration)
    {
        var instance = configuration.GetValue<string>("AzureAdB2C:Instance");
        var domain = configuration.GetValue<string>("AzureAdB2C:Domain");
        var policy = configuration.GetValue<string>("AzureAdB2C:SignUpSignInPolicyId");

        return new OpenApiSecurityScheme
        {
            Name = "oauth2",
            Type = SecuritySchemeType.OAuth2,
            In = ParameterLocation.Header,
            Scheme = "Bearer",
            Flows = new OpenApiOAuthFlows
            {
                Implicit = new OpenApiOAuthFlow
                {
                    AuthorizationUrl = new Uri($"{instance}/{domain}/oauth2/v2.0/authorize?p={policy}"),
                    TokenUrl = new Uri($"{instance}/{domain}/oauth2/v2.0/token?p={policy}"),
                    Scopes = Scopes
                }
            }
        };
    }

    private static OpenApiSecurityRequirement CreateSecurityRequirement(OpenApiSecurityScheme securityScheme)
    {
        return new OpenApiSecurityRequirement
        {
            { securityScheme, new List<string> { securityScheme.Name } }
        };
    }
}