using JonDJones.Core.ViewModel.Base;
using JonDJones.Core.ViewModel.Page;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace JonDJones.Core.Controller.Umbraco
{
    public class LoginPageController : RenderController
    {
        private IMemberManager _memberManager;

        private IPublishedValueFallback _publishedValueFallback;

        public LoginPageController(
                ILogger<LoginPageController> logger,
                ICompositeViewEngine compositeViewEngine,
                IUmbracoContextAccessor umbracoContextAccessor,
                IPublishedValueFallback publishedValueFallback,
                IMemberManager memberManager
             )
            : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
            _publishedValueFallback = publishedValueFallback;
            _memberManager = memberManager;
        }

        public override IActionResult Index()
        {
            var loginPage = new LoginPage(CurrentPage, _publishedValueFallback);
            var viewModel = new ComposedPageViewModel<LoginPage, LoginPageViewModel>
            {
                Page = loginPage,
                ViewModel = new LoginPageViewModel
                {
                    IsUserLoggedIn = _memberManager.IsLoggedIn(),
                }
            };

            return View("~/Views/LoginPage/index.cshtml", viewModel);
        }
    }
}