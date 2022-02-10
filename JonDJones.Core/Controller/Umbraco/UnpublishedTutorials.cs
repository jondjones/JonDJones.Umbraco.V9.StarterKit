using JonDJones.Core.Interfaces;
using JonDJones.Core.Resources;
using JonDJones.Core.ViewModel.Poco;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Web.Common.Authorization;
using Umbraco.Cms.Web.Common.Filters;

namespace JonDJones.Core.Controller
{
    [Authorize(Policy = AuthorizationPolicies.BackOfficeAccess)]
    [DisableBrowserCache]
    // https://github.com/umbraco/Umbraco-CMS/blob/v9/contrib/src/Umbraco.Web.Common/Controllers/UmbracoAuthorizedController.cs
    public class AdminController : Microsoft.AspNetCore.Mvc.Controller
    {
        private ISearchService _searchService;

        public AdminController(
            ISearchService searchService)
        {
            _searchService = searchService;
        }

        public IActionResult Index()
        {
            var data = _searchService.QueryInternalUmbracoIndexForUnPublishedPages();
            var filteredByPublished = data.Where(x => !x.Published);

            return View("UnpublishedTutorials", filteredByPublished.Select(x => ConvertToPoco(x)));
        }

        private LinkPoco ConvertToPoco(IContent x)
        {
            var date = x.GetValue<DateTime>(DocumentTypeConstants.PropertyAlias.PostDate);

            return new LinkPoco
            {
                Name = x.Name,
                PostDate = date,
                PostDateAsString = date.ToString("dd-MM-yyyy"),
                Url = GetUmbracoInternalUrlForPage(x.Id)
            };
        }

        private string GetUmbracoInternalUrlForPage(int id)
        {
            return "/umbraco/#/content/content/edit/" + id;
        }
    }
}