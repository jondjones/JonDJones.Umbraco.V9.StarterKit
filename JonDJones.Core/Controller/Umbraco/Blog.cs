using JonDJones.Core.ViewModel.Base;
using JonDJones.Core.ViewModel.Page;
using JonDJones.Core.ViewModel.Poco;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using Umbraco.Cms.Core.Dictionary;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco.Extensions;

namespace JonDJones.Core.Controller
{
    [ResponseCache(CacheProfileName = "Weekly")]
    [OutputCache(Profile = "default")]
    public class BlogController : RenderController
    {
        private readonly IPublishedValueFallback _publishedValueFallback;
        private readonly ICultureDictionary _dictionary;
        private readonly IRequestCultureFeature _requestCultureFeature;

        public BlogController(
                ILogger<BlogListingController> logger,
                ICompositeViewEngine compositeViewEngine,
                IUmbracoContextAccessor umbracoContextAccessor,
                IPublishedValueFallback publishedValueFallback,
                ICultureDictionaryFactory diconaryFactory,
                ILocalizationService localizationService
             )
            : base(logger, compositeViewEngine, umbracoContextAccessor)
		{
			_publishedValueFallback = publishedValueFallback;
            _dictionary = diconaryFactory.CreateDictionary();

            // Example of using localization service
            localizationService.GetAllLanguages();
        }

        public override IActionResult Index()
        {
            var blog = new Blog(CurrentPage, _publishedValueFallback);

            var diconary = _dictionary["Error"];

            var uiCultureInfo = Thread.CurrentThread.CurrentUICulture;
            var cultureInfo = Thread.CurrentThread.CurrentCulture;

            var languagePickerLinks = new List<LinkPoco>();
            foreach (var culture in blog.Cultures)
            {
                var link = new LinkPoco
                {
                    Url = blog.Url(culture.Value.Culture),
                    Name = blog.Value<string>("name", culture.Value.Culture),
                    Culture = culture.Value.Culture
                };

                languagePickerLinks.Add(link);
            }

            var viewModel = new ComposedPageViewModel<Blog, BlogViewModel>
            {
                Page = blog,
                ViewModel = new BlogViewModel
                {
                    TranslationText = diconary,
                    LanguagePickerLinks = languagePickerLinks
                }
            };

            return View("~/Views/Blog/index.cshtml", viewModel);
        }
    }
}