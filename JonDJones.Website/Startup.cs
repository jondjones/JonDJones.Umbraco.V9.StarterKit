using System;
using JonDJones.Core.Components;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Extensions;

namespace JonDJonesUmbraco9SampleSite
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _config;

        public Startup(IWebHostEnvironment webHostEnvironment, IConfiguration config)
        {
            _env = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddUmbraco(_env, _config)
                .AddBackOffice()
                .AddWebsite()
                .AddComposers()

                // Approach 2:  How to add a notification
                .AddNotificationHandler<ContentPublishingNotification, LogPushlishNotification>()
                .Build();
        }

        public void Configure(
                            IApplicationBuilder app,
                            IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                var options = new RewriteOptions();

                //.AddRedirectToHttpsPermanent()
                //.AddRedirectToWwwPermanent();
                app.UseRewriter(options);

                // Set 500 error page
                app.UseExceptionHandler("/500.html");
            }

            app.UseUmbraco()
                .WithMiddleware(u =>
                {
                    // Good practice. Define a custom redirect middleware
                    u.UseCustomRedirects();
                    u.UseBackOffice();
                    u.UseWebsite();
                })
                .WithEndpoints(u =>
                {
                    u.UseInstallerEndpoints();
                    u.UseBackOfficeEndpoints();
                    u.UseWebsiteEndpoints();

                    // Good practice. Define a custom UmbracoApplicationBuilderExtension
                    u.UseCustomRoutingRules();
                });
        }
    }
}