using JonDJones.Core.Interfaces;
using JonDJones.Core.Services;
using JonDJones.Core.ViewModel;
using JonDJones.Core.ViewModel.Base;
using JonDJones.Core.ViewModel.Page;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace JonDJones.Core.Controller
{
    public class BlogController : RenderController
    {
        private IPublishedValueFallback _publishedValueFallback;
        private IBlogService _blogService;

        public BlogController(
                ILogger<BlogListingController> logger,
                ICompositeViewEngine compositeViewEngine,
                IUmbracoContextAccessor umbracoContextAccessor,
                IPublishedValueFallback publishedValueFallback,
                IBlogService blogService
             )
            : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
            _publishedValueFallback = publishedValueFallback;
            _blogService = blogService;
        }

        public override IActionResult Index()
        {
            var blog = new Blog(CurrentPage, _publishedValueFallback);
            var viewModel = new ComposedPageViewModel<Blog, BlogViewModel>
            {
                Page = blog,
                ViewModel = new BlogViewModel()
            };

            return View("~/Views/Blog/index.cshtml", viewModel);
        }
    }
}