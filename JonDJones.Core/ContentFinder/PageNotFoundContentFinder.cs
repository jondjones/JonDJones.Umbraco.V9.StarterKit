using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;

namespace JonDJones.Core.ContentFinder
{
    /// <summary>
    /// Not best practice in real-world, used for demo purposes only
    /// Use appsettings.json
    /// </summary>
    public class PageNotFoundContentFinder : IContentLastChanceFinder
    {
        private IUmbracoContextAccessor _umbracoContextAccessor;

        public PageNotFoundContentFinder(
                IUmbracoContextAccessor umbracoContextAccessor)
        {
            _umbracoContextAccessor = umbracoContextAccessor;
        }

        public bool TryFindContent(IPublishedRequestBuilder request)
        {
            var notFoundPage = _umbracoContextAccessor.GetRequiredUmbracoContext().Content.GetById(2215);
            request.SetIs404();
            request.SetPublishedContent(notFoundPage);

            return true;
        }
    }
}
