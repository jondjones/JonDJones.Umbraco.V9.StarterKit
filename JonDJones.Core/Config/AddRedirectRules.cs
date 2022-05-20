using Microsoft.AspNetCore.Builder;
using Umbraco.Cms.Web.Common.ApplicationBuilder;

namespace JonDJonesUmbraco9SampleSite
{
    public static partial class UmbracoApplicationBuilderExtensions
    {
        public static IUmbracoApplicationBuilderContext UseCustomRedirects(
                                this IUmbracoApplicationBuilderContext builder)
        {
            builder.AppBuilder.Use(async (context, next) =>
            {
                var url = context.Request.Path.Value;
                if (url.EndsWith(@"\"))
                {
                    context.Response.Redirect(url.TrimEnd(new[] { '/' }));
                    return;
                }
                if (url.EndsWith(@"exception"))
                {
                    context.Response.Redirect("/");
                    return;
                }

                await next();
            });

            return builder;
        }
    }
}