using JonDJones.Core.Interfaces;
using JonDJones.Core.Interfaces.Example;
using JonDJones.Core.ViewModel;
using JonDJones.Core.ViewModel.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
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

        public HomeController(
                ILogger<HomeController> logger,
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

    public interface ISingleteon
    {
    }
}