// -----------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using MicrosoftGraph_Security_API_Sample.Extensions;
using MicrosoftGraph_Security_API_Sample.Helpers;
using MicrosoftGraph_Security_API_Sample.Models;
using MicrosoftGraph_Security_API_Sample.Models.Configurations;
using MicrosoftGraph_Security_API_Sample.Providers;
using MicrosoftGraph_Security_API_Sample.Services;
using MicrosoftGraph_Security_API_Sample.Services.Interfaces;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;

namespace MicrosoftGraph_Security_API_Sample
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AzureConfiguration _azureConfiguration = new AzureConfiguration();
            Configuration.Bind("AzureAd", _azureConfiguration);

            CacheTime _cacheTime = new CacheTime();
            Configuration.Bind("CacheTime", _cacheTime);

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
                    });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddAzureAdBearer(options => Configuration.Bind("AzureAd", options));

            services.AddMvc(options =>
                {
                    /// add custom binder to beginning of collection
                    options.ModelBinderProviders.Insert(0, new AlertFilterValueBinderProvider());
                    /// add cach profiles from appsettings.json
                    options.AddCachingsProfiles(_cacheTime);
                })
                .AddJsonOptions(opt => opt.SerializerSettings
                        .ContractResolver = new CamelCasePropertyNamesContractResolver())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddMemoryCache();
            services.AddScoped<ITokenProvider, TokenProvider>();
            services.AddScoped<IAlertService, AlertService>();
            services.AddScoped<IActionService, ActionService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddSingleton<IDemoExample>(instance =>
            {
                return new DemoExample(Convert.ToBoolean(Configuration["UseMockFilters"]));
            });
            services.AddScoped<IGraphServiceProvider>(instance =>
               {
                   return new GraphServiceProvider(azureConfiguration: _azureConfiguration);
               }); ;
            services.AddSingleton<IMemoryCacheHelper, MemoryCacheHelper>();
            services.AddSignalR();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "wwwroot";
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v3", new Info { Title = "MicrosoftGraph_Security_API_Sample", Version = "v3" });
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", new string[] { } }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("AllowAllOrigins");
            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Scripts"))
            });
            app.UseSpaStaticFiles();
            app.UseAuthentication();
            app.UseSwagger();
            app.UseStatusCodePages();
            app.UseSignalR(routes =>
            {
                routes.MapHub<NotificationHub>("/notifications");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v3/swagger.json", "MicrosoftGraph_Security_API_Sample V3");
            });

            // enable SPA service if it is client dev environment
            var clientEnv = Environment.GetEnvironmentVariable("CLIENT_ENVIRONMENT");
            if (clientEnv == "Development")
            {
                app.UseSpa(spa =>
                {
                    // To learn more about options for serving an Angular SPA from ASP.NET Core,
                    // see https://go.microsoft.com/fwlink/?linkid=864501

                    spa.Options.SourcePath = "ClientApp";

                    if (env.IsDevelopment())
                    {
                        spa.UseAngularCliServer(npmScript: "start");
                    }
                });
            }

            app.Run(async (context) =>
            {
                context.Response.ContentType = "text/html";
                await context.Response.SendFileAsync(Path.Combine(env.WebRootPath, "index.html"));
            });
        }
    }
}