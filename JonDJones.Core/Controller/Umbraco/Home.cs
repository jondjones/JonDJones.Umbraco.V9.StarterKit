using JonDJones.Core.Interfaces;
using JonDJones.Core.Services;
using JonDJones.Core.ViewModel;
using JonDJones.Core.ViewModel.Base;
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
    public class HomeController : RenderController
    {
        private IPublishedValueFallback _publishedValueFallback;
        private IBlogService _blogService;
        private IMenuService _menuService;

        public HomeController(
                ILogger<HomeController> logger,
                ICompositeViewEngine compositeViewEngine,
                IUmbracoContextAccessor umbracoContextAccessor,
                IPublishedValueFallback publishedValueFallback,
                IBlogService blogService,
                IMenuService menuService
             )
            : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
            _publishedValueFallback = publishedValueFallback;
            _blogService = blogService;
            _menuService = menuService;
        }

        public override IActionResult Index()
        {
            var home = new Home(CurrentPage, _publishedValueFallback);
            var viewModel = new ComposedPageViewModel<Home, HomeViewModel>
            {
                Page = home,
                ViewModel = new HomeViewModel
                {
                    Blogs = _blogService.Blogs
                }
            };

            return View("~/Views/Home/index.cshtml", viewModel);
        }
    }
}