using System.Collections.Generic;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JonDJones.Core.Interfaces
{
    public interface ISearchService
    {
        IEnumerable<IPublishedContent> QueryUmbraco(string searchTerm);

        IEnumerable<IContent> QueryInternalUmbracoIndexForUnPublishedPages();
    }
}
