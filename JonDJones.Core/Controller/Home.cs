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

        public HomeController(
                ILogger<HomeController> logger,
                ICompositeViewEngine compositeViewEngine,
                IUmbracoContextAccessor umbracoContextAccessor,
                IPublishedValueFallback publishedValueFallback
             )
            : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
            _publishedValueFallback = publishedValueFallback;
        }

        public override IActionResult Index()
        {
            var home = new Home(CurrentPage, _publishedValueFallback);
            return View("~/Views/Home/index.cshtml", home);
        }
    }
}