// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using MicrosoftGraph_Security_API_Sample.Models.Configurations;

namespace MicrosoftGraph_Security_API_Sample.Extensions
{
    public static class AzureAdServiceCollectionExtensions
    {
        public static AuthenticationBuilder AddAzureAdBearer(this AuthenticationBuilder builder)
            => builder.AddAzureAdBearer(_ => { });

        public static AuthenticationBuilder AddAzureAdBearer(this AuthenticationBuilder builder, Action<AzureConfiguration> configureOptions)
        {
            builder.Services.Configure(configureOptions);
            builder.Services.AddSingleton<IConfigureOptions<JwtBearerOptions>, ConfigureAzureOptions>();
            builder.AddJwtBearer();
            return builder;
        }

        private class ConfigureAzureOptions : IConfigureNamedOptions<JwtBearerOptions>
        {
            private readonly AzureConfiguration _azureOptions;

            public ConfigureAzureOptions(IOptions<AzureConfiguration> azureOptions)
            {
                _azureOptions = azureOptions.Value;
            }

            public void Configure(string name, JwtBearerOptions options)
            {
                options.Audience = _azureOptions.ClientId;

                options.Authority = $"{_azureOptions.Instance}common/v2.0";

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                };

                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";
                        var err = context.Exception.ToString();
                        var result = JsonConvert.SerializeObject(new { err });
                        return context.Response.WriteAsync(result);
                    }
                };
            }

            public void Configure(JwtBearerOptions options)
            {
                Configure(Options.DefaultName, options);
            }
        }
    }
}