using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace JonDJones.Core.ViewComponents
{
    public class bannerViewComponent : ViewComponent
    {
        private IPublishedValueFallback _publishedValueFallback;

        public bannerViewComponent(
                IPublishedValueFallback publishedValueFallback)
        {
            _publishedValueFallback = publishedValueFallback;
        }

        public async Task<IViewComponentResult> InvokeAsync(IPublishedElement model)
        {
            var banner = new Banner(model, _publishedValueFallback);
            return View(banner);
        }
    }
}
