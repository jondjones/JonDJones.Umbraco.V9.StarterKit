using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace JonDJones.Core.ViewComponents
{
    public class cardItemViewComponent : ViewComponent
    {
        private IPublishedValueFallback _publishedValueFallback;

        public cardItemViewComponent(
                IPublishedValueFallback publishedValueFallback)
        {
            _publishedValueFallback = publishedValueFallback;
        }

        public async Task<IViewComponentResult> InvokeAsync(IPublishedElement model)
        {
            var banner = new CardItem(model, _publishedValueFallback);
            return View(banner);
        }
    }
}