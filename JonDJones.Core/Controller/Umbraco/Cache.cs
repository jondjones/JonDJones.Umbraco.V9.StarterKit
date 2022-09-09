using JonDJones.Core.Interfaces;
using JonDJones.Core.ViewModel.Base;
using JonDJones.Core.ViewModel.Page;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace JonDJones.Core.Controller
{
    public class CacheController : RenderController
    {
        private IPublishedValueFallback _publishedValueFallback;
        private readonly IMemoryCache _cache;

        public CacheController(
                ILogger<SearchController> logger,
                ICompositeViewEngine compositeViewEngine,
                IUmbracoContextAccessor umbracoContextAccessor,
                IPublishedValueFallback publishedValueFallback,
                IMemoryCache memoryCache
             )
            : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
            _publishedValueFallback = publishedValueFallback;
            _cache = memoryCache;
        }

        public override IActionResult Index()
        {
            var cacheKey = "myKey";

            if (!_cache.TryGetValue(cacheKey, out string cacheEntry))
            {
                cacheEntry = DateTime.Now.ToString();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(30))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(30));

                _cache.Set(cacheKey, cacheEntry, cacheEntryOptions);
            }

            var cache = new Cache(CurrentPage, _publishedValueFallback);
            var viewModel = new ComposedPageViewModel<Cache, CacheViewModel>
            {
                Page = cache,
                ViewModel = new CacheViewModel
                {
                    Date = cacheEntry
                }
            };
            return View("~/Views/Cache/index.cshtml", viewModel);
        }
    }
}