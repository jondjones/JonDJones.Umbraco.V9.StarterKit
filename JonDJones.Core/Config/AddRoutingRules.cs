using Microsoft.AspNetCore.Builder;
using System.Web.Mvc;
using Umbraco.Cms.Web.Common.ApplicationBuilder;
using Umbraco.Extensions;

namespace JonDJonesUmbraco9SampleSite
{
    public static partial class UmbracoApplicationBuilderExtensions
    {
        public static IUmbracoEndpointBuilderContext UseCustomRoutingRules(this IUmbracoEndpointBuilderContext app)
        {
            if (!app.RuntimeState.UmbracoCanBoot())
            {
                return app;
            }

            app.EndpointRouteBuilder.MapControllerRoute(
                           "vanilla-route",
                           "/vanilla/{action}/{id?}",
                           new { Controller = "Vanilla", Action = "Index" });

            app.EndpointRouteBuilder.MapControllerRoute(
                           "secure-route",
                           "umbraco/backoffice/Plugins/Backend/Index",
                           new { Controller = "Backend", Action = "Index" });


            app.EndpointRouteBuilder.MapControllerRoute(
                           "AdminDefaul",
                           "umbraco/backoffice/plugins/admin/unpublishedtutorials",
                           new { Controller = "Admin", Action = "Index" });

            app.EndpointRouteBuilder.MapControllerRoute(
              "XmlSitemap",
              "sitemap",
              new
              {
                  controller = "XmlSitemap",
                  action = "Index",
                  id = UrlParameter.Optional
              });

            return app;
        }
    }
}
