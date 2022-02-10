using JonDJones.Core.Interfaces;
using JonDJones.Core.ViewModel.Base;
using JonDJones.Core.ViewModel.Page;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using System.Linq;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace JonDJones.Core.Controller
{
    public class SearchController : RenderController
    {
        private IPublishedValueFallback _publishedValueFallback;
        private ISearchService _searchService;

        public SearchController(
                ILogger<SearchController> logger,
                ICompositeViewEngine compositeViewEngine,
                IUmbracoContextAccessor umbracoContextAccessor,
                IPublishedValueFallback publishedValueFallback,
                ISearchService searchService
             )
            : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
            _publishedValueFallback = publishedValueFallback;
            _searchService = searchService;
        }

        public IActionResult Search(string searchTerm, int page = 1)
        {
            var searchPage = new Search(CurrentPage, _publishedValueFallback);
            var results = _searchService.QueryUmbraco(searchTerm);

            var viewModel = new ComposedPageViewModel<Search, SearchViewModel>
            {
                Page = searchPage,
                ViewModel = new SearchViewModel
                {
                    Results = results,
                    DisplayResults = results != null && results.Any()
                }
            };

            return View("~/Views/Search/index.cshtml", viewModel);
        }
    }
}