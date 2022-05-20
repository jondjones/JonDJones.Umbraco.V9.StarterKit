using System;
using System.Collections.Generic;
using Umbraco.Cms.Core.Hosting;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.ContentEditing;
using Umbraco.Cms.Core.Models.Membership;
using Umbraco.Cms.Core.Routing;

namespace JonDJones.Core.ContentApps
{
    public class AdditionalLinksContentApp : IContentAppFactory
    {
        IHostingEnvironment _hostingEnvironment;
        UriUtility _uriUtility;

        public AdditionalLinksContentApp(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _uriUtility = new UriUtility(_hostingEnvironment);

        }
        public ContentApp GetContentAppFor(
            object source,
            IEnumerable<IReadOnlyUserGroup> userGroups)
        {
            var content = source as IContent;
            if (content != null)
            {
                return new ContentApp
                {
                    Alias = "additionalLinksApp",
                    Name = "Links",
                    Icon = "icon-cloud",
                    View = _uriUtility.ToAbsolute($"/umbraco/backoffice/plugins/contentapp/links/{content.Id}"),
                    Weight = -100,
                    Active = true
                };
            }

            return null;
        }
    }
}