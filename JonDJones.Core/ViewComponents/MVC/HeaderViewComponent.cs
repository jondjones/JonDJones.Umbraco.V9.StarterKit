using JonDJones.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco.Extensions;

namespace JonDJones.Core.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private IMenuService _menu;
        private IUmbracoContextAccessor _context;
        private string DEFAULT_TEXT = "A RESPONSIVE HTML5 SITE TEMPLATE.MANUFACTURED BY HTML5 UP.";

        public HeaderViewComponent(
                IMenuService menu,
                IUmbracoContextAccessor context)
        {
            _context = context;
            _menu = menu;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var content = _context.GetRequiredUmbracoContext().PublishedRequest.PublishedContent;
            var keyword = content.Value<string>("keyword");
            var displayHeader = content.Value<bool>("displayHeader");
            var header = new HeaderViewModel
            {
                MenuItems = _menu.Menus,
                Title = content.Name,
                SubTitle = keyword == null || keyword.IsNullOrWhiteSpace() ? DEFAULT_TEXT : keyword,
                DisplayHeader = displayHeader
            };

            return View(header);
        }
    }
}
