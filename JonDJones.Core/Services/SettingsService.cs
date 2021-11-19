using JonDJones.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco.Extensions;

namespace JonDJones.Core.Services
{
    public class SettingsService : ISettingsService
    {
        private IUmbracoContextFactory _umbracoContextFactory;

        public SettingsService(
            IUmbracoContextFactory umbracoContextFactory)
        {
            _umbracoContextFactory = umbracoContextFactory;
        }

        public Settings SettingsPage => GetSettings();

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
