using JonDJones.Core.Interfaces;
using System.Linq;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco.Extensions;

namespace JonDJones.Core.Services
{
    public class SettingsService : ISettingsService
    {
        private IUmbracoContextFactory _umbracoContextFactory;
        private IPublishedValueFallback _publishedValueFallback;

        public SettingsService(
            IUmbracoContextFactory umbracoContextFactory,
            IPublishedValueFallback publishedValueFallback)
        {
            _umbracoContextFactory = umbracoContextFactory;
            _publishedValueFallback = publishedValueFallback;
        }

        public Settings SettingsPage => GetSettings();

        public Home Homepage => GetHomePage();

        public string HompageAbsoluteUrl => Homepage.Url(null, UrlMode.Absolute);

        private Home GetHomePage()
        {
            using (var umbracoContextReference = _umbracoContextFactory.EnsureUmbracoContext())
            {
                var root = umbracoContextReference.UmbracoContext?.Content.GetByRoute("/", false);
                return new Home(root, _publishedValueFallback);
            }
        }

        private Settings GetSettings()
        {
            using (var cref = _umbracoContextFactory.EnsureUmbracoContext())
            {
                var cache = cref.UmbracoContext.Content;
                return cache.GetAtRoot().DescendantsOrSelf<Settings>().First();
            }
        }

    }
}
