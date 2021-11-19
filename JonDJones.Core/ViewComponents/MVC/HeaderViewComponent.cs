using JonDJones.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace JonDJones.Core.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private IMenuService _menu;

        public HeaderViewComponent(
                IMenuService menu)
        {
            _menu = menu;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var header = new HeaderViewModel
            {
                MenuItems = _menu.Menus
            };

            return View(header);
        }
    }
}
