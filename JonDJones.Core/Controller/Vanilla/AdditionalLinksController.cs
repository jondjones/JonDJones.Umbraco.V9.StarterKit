using JonDJones.Core.Interfaces;
using System.Text;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;

namespace JonDJones.Core.Controller.Vanilla
{
    public class AdditionalLinksController : Microsoft.AspNetCore.Mvc.Controller
    {
        private IContentService _contentService;
        private IUmbracoContextAccessor _umbracoContextAccessor;
        private ISettingsService _settingsService;
        private IPublishedContentCache _cache;

        public AdditionalLinksController(
            IContentService contentService,
            IUmbracoContextAccessor umbracoContextAccessor,
            ISettingsService settingsService)
        {
            _contentService = contentService;
            _umbracoContextAccessor = umbracoContextAccessor;
            _settingsService = settingsService;

            _cache = _umbracoContextAccessor.GetRequiredUmbracoContext().Content;
        }

        public Microsoft.AspNetCore.Mvc.ViewResult Index(int id)
        {
            var viewModel = new AdditionalLinkViewModel();
            var item = _cache.GetById(id);
            if (item == null)
            {
                var urlInProgress = new StringBuilder();

                var unPublisheditem = _contentService.GetById(id);
                var parentitem = _cache.GetById(unPublisheditem.ParentId);

                var mainUrl = parentitem.Url(null, UrlMode.Absolute);
                var previewItem = _cache.GetById(true, id);

                urlInProgress.Append($"{mainUrl}/");
                urlInProgress.Append(previewItem.UrlSegment);

                viewModel.LinkUrl = urlInProgress.ToString();
                viewModel.PublishedStatus = "Not Published";

                //foreach (var path in unPublisheditem.Path.Split(','))
                //{
                //    var segment = _cache.GetById(Convert.ToInt32(path));
                //    if (segment == null || segment.Level == 1)
                //    {
                //        continue;
                //    }
                //    urlInProgress.Append($"{segment.UrlSegment}/");
                //}
            }
            else
            {
                viewModel.LinkUrl = item.Url(null, UrlMode.Absolute);
            }

            return View(viewModel);
        }
    }
}