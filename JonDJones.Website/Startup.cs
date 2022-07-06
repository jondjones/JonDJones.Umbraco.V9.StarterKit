using System;
using System.Collections.Generic;
using System.Globalization;
using JonDJones.Core.Components;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Smidge;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using WebEssentials.AspNetCore.OutputCaching;

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

            // Performance
            services.AddSmidge(_config.GetSection("smidge"));

            // Response Caching
            services.AddResponseCaching();
            services.AddMvc(options =>
            {
                options.CacheProfiles.Add("Weekly", new CacheProfile()
                {
                    Duration = 60 * 60 * 24 * 7
                });
            });

            // Output caching
            services.AddOutputCaching(options =>
            {
                options.Profiles["default"] = new OutputCacheProfile
                {
                    Duration = 60 * 60 * 24 * 7
                };
            });
        }

        public void Configure(
                            IApplicationBuilder app,
                            IWebHostEnvironment env)
        {
            SetLanguageCultures(app);

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

            // Performance configuration - Bundling
            app.UseRouting();
            app.UseSmidge(bundles =>
            {
                bundles.CreateJs("js-script", "~/assets/js");
                bundles.CreateCss("css-script", "~/assets/css");
            });

            // Output caching
            app.UseOutputCaching();

            // Response Caching
            app.UseResponseCaching();

            // Performance configuration - Static assets
            app.UseStaticFiles(new StaticFileOptions
            {
                HttpsCompression = Microsoft.AspNetCore.Http.Features.HttpsCompressionMode.Compress,
                OnPrepareResponse = (context) =>
                {
                    var headers = context.Context.Response.GetTypedHeaders();
                    headers.CacheControl = new CacheControlHeaderValue
                    {
                        Public = true,
                        MaxAge = TimeSpan.FromMinutes(1)
                    };
                }
            });

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

        private static void SetLanguageCultures(IApplicationBuilder app)
        {
            var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en-US"),
                new CultureInfo("ne"),
            };
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"), //English US will be the default culture (for new visitors)
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };

            app.UseRequestLocalization(localizationOptions);
        }
    }
}